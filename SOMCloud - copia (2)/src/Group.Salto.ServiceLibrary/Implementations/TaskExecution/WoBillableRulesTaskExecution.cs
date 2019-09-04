using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Contracts.Analysis;
using Group.Salto.ServiceLibrary.Common.Contracts.TaskExecution;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Enums;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common;
using Group.Salto.Common.Constants.Templates;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.ServiceLibrary.Common.Contracts.Ncalc;
using Group.Salto.ServiceLibrary.Common.Contracts.Templates;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.TemplateProcessor;

namespace Group.Salto.ServiceLibrary.Implementations.TaskExecution
{
    public class WoBillableRulesTaskExecution : IWoBillableRulesTaskExecution
    {
        private readonly IAnalysisService _analysisService;
        private readonly IItemsRepository _itemsRepository;
        private readonly ITemplateProcessorService _templateProcessorService;
        private readonly INcalcService _ncalcService;
        private readonly IPeopleRepository _peopleRepository;

        public WoBillableRulesTaskExecution(IAnalysisService analysisService,
                                            IItemsRepository itemsRepository,
                                            ITemplateProcessorService templateProcessorService,
                                            INcalcService ncalcService,
                                            IPeopleRepository peopleRepository)
        {
            _analysisService = analysisService;
            _itemsRepository = itemsRepository;
            _templateProcessorService = templateProcessorService;
            _ncalcService = ncalcService;
            _peopleRepository = peopleRepository;
        }

        public ResultDto<bool> PerformTask(TaskExecutionValues taskExecutionValues)
        {
            try
            {
                var bills = ApplyBillableRules(taskExecutionValues);
                foreach (var bill in bills)
                {
                    if (bill?.BillLine != null && bill.BillLine.Count > 0)
                    {
                        taskExecutionValues.CurrentWorkOrder.Bill.Add(bill);
                    }
                }
            }
            catch (Exception e)
            {
                taskExecutionValues.Result.Data = false;
                taskExecutionValues.Result.Errors = new ErrorsDto { Errors = new List<ErrorDto>{ new ErrorDto { ErrorType = ErrorType.TaskExecutionProcessError, ErrorMessageKey = e.ToString() } } };
            }

            return taskExecutionValues.Result;
        }

        private IEnumerable<Bill> ApplyBillableRules(TaskExecutionValues taskExecutionValues)
        {
            var bills = new List<Bill>();
            var pendingState = taskExecutionValues.CreatedService.FormState == FormState.ValidationPending.ToString() || taskExecutionValues.CreatedService.FormState == FormState.DeliveringPending.ToString();
            if (pendingState)
            {
                bills = CreatePendingStateBills(taskExecutionValues).ToList();
            }

            foreach (var billingRule in taskExecutionValues.CurrentTask.BillingRule)
            {
                var billingRuleProcessorValues = new TemplateProcessorValuesDto
                {
                    Template = billingRule.Condition,
                    WorkOrder = taskExecutionValues.CurrentWorkOrder,
                    Service = taskExecutionValues.CreatedService,
                    NullStringValue = TemplateProcessorConstants.BillTemplateNullStringValue,
                    NcalcEncapsulate = true
                };

                var billingRuleProcessorResult = _templateProcessorService.ProcessTemplate(billingRuleProcessorValues);

                var condResult = _ncalcService.EvaluateExpression(billingRuleProcessorResult);

                var expressionResult = CheckNcalcExpresion(condResult);
                if (expressionResult)
                {
                    ProcessCurrentBillingRule(taskExecutionValues, bills, billingRule);
                }
            }

            return bills;
        }

        private void ProcessCurrentBillingRule(TaskExecutionValues taskExecutionValues, List<Bill> bills, BillingRule billingRule)
        {
            var bill = new Bill
            {
                Date = DateTime.UtcNow,
                People = taskExecutionValues.CurrentPeople,
                Status = (int) BillStatus.Pending,
                Task = taskExecutionValues.CurrentTask.Name,
                TaskId = taskExecutionValues.CurrentTask.Id,
                BillLine = new List<BillLine>(),
                Service = taskExecutionValues.CreatedService,
                UpdateDate = DateTime.UtcNow
            };

            if (taskExecutionValues.TaskParameters.ResponsibleId > 0)
            {
                var people = _peopleRepository.GetByIdWithSubContractCompanyAndCost(taskExecutionValues.TaskParameters.ResponsibleId);
                bill.PeopleId = taskExecutionValues.TaskParameters.ResponsibleId;
                bill.People = people;
            }

            bill.Date = GetBillDate(taskExecutionValues.CurrentWorkOrder, taskExecutionValues.CreatedService);
            taskExecutionValues.CurrentWorkOrder.Bill.Add(bill);

            if (taskExecutionValues.CreatedService.PredefinedService?.MustValidate != null && taskExecutionValues.CreatedService.PredefinedService.MustValidate.Value)
            {
                bill.Status = (int) BillStatus.ValidationPending;
            }

            if (bills.Any(b => b.ErpSystemInstanceId == billingRule.ErpSystemInstanceId))
            {
                bill = bills.First(b => b.ErpSystemInstanceId == billingRule.ErpSystemInstanceId);
            }
            else
            {
                bill.ErpSystemInstanceId = billingRule.ErpSystemInstanceId;
                bills.Add(bill);
            }

            foreach (var billingRuleItem in billingRule.BillingRuleItem)
            {
                ProcessCurrentBillingRuleItem(taskExecutionValues.CurrentWorkOrder, taskExecutionValues.CreatedService, billingRuleItem, bill);
            }
        }

        private void ProcessCurrentBillingRuleItem(WorkOrders currentWorkOrder, Services createdService, BillingRuleItem billingRuleItem, Bill bill)
        {
            var billingRuleItemProcessorValues = new TemplateProcessorValuesDto
            {
                Template = billingRuleItem.Units,
                WorkOrder = currentWorkOrder,
                Service = createdService,
                NullStringValue = TemplateProcessorConstants.BillTemplateNullStringValue,
                NcalcEncapsulate = true
            };

            var billingRuleItemProcessorResult = _templateProcessorService.ProcessTemplate(billingRuleItemProcessorValues);

            var unitsNcalc = _ncalcService.EvaluateBillRuleItemExpression(billingRuleItemProcessorResult);

            var units = GetDoubleNcalcExpression(unitsNcalc);
            if (units != null && units.Value > 0)
            {
                var line = new BillLine
                {
                    ItemId = billingRuleItem.ItemId,
                    Units = units.Value,
                    Bill = bill,
                    UpdateDate = DateTime.UtcNow
                };
                bill.BillLine.Add(line);
            }
        }

        private IEnumerable<Bill> CreatePendingStateBills(TaskExecutionValues taskExecutionValues)
        {
            var bills = new List<Bill>();
            var materialExtraFieldValues = taskExecutionValues.CreatedService.ExtraFieldsValues.Where(efv => efv.MaterialForm != null && efv.MaterialForm.Any());
            foreach (var extraFieldValue in materialExtraFieldValues)
            {
                var bill = GetNewOrExistingBill(taskExecutionValues, extraFieldValue, bills);

                var addedItems = new List<Entities.Tenant.Items>();
                foreach (var materialForm in extraFieldValue.MaterialForm)
                {
                    var billLine = new BillLine
                    {
                        Units = materialForm.Units ?? 0,
                        Bill = bill,
                        UpdateDate = DateTime.UtcNow
                    };
                    var item = _itemsRepository.GetByErpReference(materialForm.Reference);
                    if (item != null)
                    {
                        AddNewItemSerialNumber(item, materialForm, billLine);
                    }
                    else if (addedItems.Any(i => i.ErpReference == materialForm.Reference))
                    {
                        var newItem = addedItems.First(i => i.ErpReference == materialForm.Reference);
                        AddNewItemSerialNumber(newItem, materialForm, billLine);
                    }
                    else
                    {
                        AddNewItem(addedItems, materialForm, billLine);
                    }

                    bill.BillLine.Add(billLine);
                }
            }

            return bills;
        }

        private Bill GetNewOrExistingBill(TaskExecutionValues taskExecutionValues, ExtraFieldsValues extraFieldValue, List<Bill> bills)
        {
            var bill = new Bill
            {
                Date = taskExecutionValues.CreatedService.CreationDate == DateTime.MinValue ? DateTime.UtcNow : taskExecutionValues.CreatedService.CreationDate,
                ErpSystemInstanceId = extraFieldValue.ExtraField.ErpSystemInstanceQuery?.ErpSystemInstanceId ?? 0,
                Service = taskExecutionValues.CreatedService,
                Workorder = taskExecutionValues.CurrentWorkOrder,
                People = taskExecutionValues.CurrentPeople,
                Status = taskExecutionValues.CreatedService.FormState == FormState.ValidationPending.ToString() ? (int) BillStatus.ValidationPending : (int) BillStatus.Pending,
                Task = taskExecutionValues.CurrentTask.Name,
                TaskId = taskExecutionValues.CurrentTask.Id,
                BillLine = new List<BillLine>(),
                UpdateDate = DateTime.UtcNow
            };

            if (taskExecutionValues.TaskParameters.ResponsibleId > 0)
            {
                var people = _peopleRepository.GetByIdWithSubContractCompanyAndCost(taskExecutionValues.TaskParameters.ResponsibleId);
                bill.PeopleId = taskExecutionValues.TaskParameters.ResponsibleId;
                bill.People = people;
            }

            if (bills.Any(b => b.ErpSystemInstanceId == bill.ErpSystemInstanceId))
            {
                bill = bills.First(b => b.ErpSystemInstanceId == bill.ErpSystemInstanceId);
            }
            else
            {
                bills.Add(bill);
            }

            return bill;
        }

        public DateTime GetBillDate(WorkOrders workOrder, Services service)
        {
            DateTime date;
            var endDate = _analysisService.GetFormValue<DateTime?>(ExtraFieldSystemTypeEnum.Enddate, service);
            if (endDate != null || workOrder.ClosingOtdate != null)
            {
                date = workOrder.ClosingOtdate ?? endDate.Value;
            }
            else
            {
                date = workOrder.ActionDate ?? service.CreationDate;
            }
            return date;
        }

        private void AddNewItemSerialNumber(Entities.Tenant.Items item, MaterialForm materialForm, BillLine billLine)
        {
            if (!string.IsNullOrWhiteSpace(materialForm.SerialNumber))
            {
                if (item.ItemsSerialNumber.All(isn => isn.SerialNumber != materialForm.SerialNumber))
                {
                    item.ItemsSerialNumber.Add(new ItemsSerialNumber
                    {
                        Item = item,
                        SerialNumber = materialForm.SerialNumber
                    });
                }
                billLine.Item = item;
                billLine.SerialNumber = materialForm.SerialNumber;
            }
            else
            {
                billLine.Item = item;
                billLine.SerialNumber = null;
            }
        }

        private void AddNewItem(List<Entities.Tenant.Items> addedItems, MaterialForm materialForm, BillLine billLine)
        {
            var newItem = new Entities.Tenant.Items
            {
                ErpReference = materialForm.Reference,
                Description = materialForm.Description,
                Name = materialForm.Reference,
                SyncErp = true,
                Type = (int)ItemType.Product,
                UpdateDate = DateTime.UtcNow
            };
            if (!string.IsNullOrWhiteSpace(materialForm.SerialNumber))
            {
                newItem.ItemsSerialNumber.Add(new ItemsSerialNumber
                {
                    SerialNumber = materialForm.SerialNumber
                });
                billLine.SerialNumber = materialForm.SerialNumber;
            }
            billLine.Item = newItem;
            addedItems.Add(newItem);
        }

        private bool CheckNcalcExpresion(object condResult)
        {
            var result = true;
            if (condResult is int integerValue)
            {
                if (1 > integerValue)
                {
                    result = false;
                }
            }
            else if (condResult is double doubleValue)
            {
                if (1 > doubleValue)
                {
                    result = false;
                }
            }
            else if (condResult is bool boolValue)
            {
                if (!boolValue)
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }
            return result;
        }

        private double? GetDoubleNcalcExpression(object unitsNcalc)
        {
            double? value = null;
            if (unitsNcalc is int integerValue)
            {
                value = integerValue * 1.0;
            }
            else if (unitsNcalc is double doubleValue)
            {
                value = doubleValue;
            }
            else if (unitsNcalc is string stringValue)
            {
                if (double.TryParse(stringValue, out var doubleParseValue))
                {
                    value = doubleParseValue;
                }
            }

            return value;
        }
    }
}
