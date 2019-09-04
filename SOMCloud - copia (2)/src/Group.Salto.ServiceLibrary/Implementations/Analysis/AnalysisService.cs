using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common;
using Group.Salto.Common.Constants.ExtraFields;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Contracts.Analysis;
using Group.Salto.ServiceLibrary.Common.Contracts.ClosingCode;
using Group.Salto.ServiceLibrary.Common.Contracts.ExtraFields;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Analysis;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Enums;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Service;

namespace Group.Salto.ServiceLibrary.Implementations.Analysis
{
    public class AnalysisService : IAnalysisService
    {
        private readonly IExtraFieldsService _extraFieldsService;
        private readonly IClosingCodeService _closingCodeService;
        
        public AnalysisService(IExtraFieldsService extraFieldsService,
                                IClosingCodeService closingCodeService)
        {
            _extraFieldsService = extraFieldsService;
            _closingCodeService = closingCodeService;
        }

        public ResultDto<bool> AddAllServicesToAnalize(WorkOrders currentWorkOrder, Entities.Tenant.People currentPeople)
        {
            var result = new ResultDto<bool> { Data = true };
            try
            {
                foreach (Services currentService in currentWorkOrder.Services)
                {
                    AddOrUpdateServiceAnalysis(currentWorkOrder, currentService, currentPeople);
                }
            }
            catch (Exception e)
            {
                result.Data = false;
                result.Errors = new ErrorsDto{Errors = new List<ErrorDto> { new ErrorDto{ErrorType = ErrorType.ValidationError, ErrorMessageKey = e.ToString()}}};
            }

            return result;
        }

        public void AddOrUpdateServiceAnalysis(WorkOrders currentWorkOrder, Services currentService, Entities.Tenant.People currentPeople)
        {
            var currentAnalysis = currentWorkOrder.ServicesAnalysis.FirstOrDefault(s => s.ServiceCode == currentService.Id);
            if (currentAnalysis == null)
            {
                AddServiceToAnalyze(currentWorkOrder, currentService, currentPeople);
            }
            else
            {
                UpdateServiceToAnalyze(currentService, currentAnalysis, currentPeople);
            }
        }

        public DateTime? GetServiceClosingDate(Services currentService)
        {
            DateTime? closingDate = null;
            var startClosingDate = GetFormValue<DateTime?>(ExtraFieldSystemTypeEnum.Startclosingdate, currentService);
            var endClosingDate = GetFormValue<DateTime?>(ExtraFieldSystemTypeEnum.Endclosingdate, currentService);

            if (startClosingDate != null && endClosingDate != null)
            {
                closingDate = endClosingDate;
            }
            return closingDate;
        }

        public List<ServiceFieldErrorEnum> ValidateServiceFields(WorkOrders currentWorkOrder, Services currentService)
        {
            var errorList = new List<ServiceFieldErrorEnum>();

            var validationValues = GetServiceValidationFieldsValues(currentService);

            if(validationValues.FinalInitDate != null && validationValues.FinalEndDate != null)
            {
                var duration = validationValues.FinalEndDate.Value.Subtract(validationValues.FinalInitDate.Value);
                if (validationValues.FinalInitDate < currentWorkOrder.CreationDate)
                {
                    errorList.Add(ServiceFieldErrorEnum.StartDatBeforeWorkOrderDate);
                }
                if (validationValues.FinalEndDate <= validationValues.FinalInitDate)
                {
                    errorList.Add(ServiceFieldErrorEnum.StartDatBeforeEndDate);
                }
                if (duration.TotalMinutes < validationValues.FinalWaitTime)     
                {
                    errorList.Add(ServiceFieldErrorEnum.WaitTimeBiggerThanDuration);
                }
                if (validationValues.FinalIsIntervention != null && validationValues.FinalIsIntervention.Value && duration.TotalDays > 1)
                {
                    errorList.Add(ServiceFieldErrorEnum.InterventionTimeMoreThanOneDay);
                }
            }

            if (validationValues.Kilometers.HasValue && validationValues.TravelTime.HasValue)
            {
                if (validationValues.Kilometers.Value <= 0 || validationValues.TravelTime.Value <= 0)
                {
                    errorList.Add(ServiceFieldErrorEnum.NegativeTravelOrKilometers);
                }
                else
                {
                    var averageSpeed = validationValues.Kilometers.Value * 60 / validationValues.TravelTime.Value;
                    if (averageSpeed > 140)
                    {
                        errorList.Add(ServiceFieldErrorEnum.NegativeTravelOrKilometers);
                    }
                }
            }

            return errorList;
        }

        private ValidateServiceFieldsValuesDto GetServiceValidationFieldsValues(Services currentService)
        {
            var valuesDto = new ValidateServiceFieldsValuesDto();

            valuesDto.StartDate = GetFormValue<DateTime?>(ExtraFieldSystemTypeEnum.Startdate, currentService);
            valuesDto.EndDate = GetFormValue<DateTime?>(ExtraFieldSystemTypeEnum.Enddate, currentService);
            valuesDto.StartClosingDate = GetFormValue<DateTime?>(ExtraFieldSystemTypeEnum.Startclosingdate, currentService);
            valuesDto.EndClosingDate = GetFormValue<DateTime?>(ExtraFieldSystemTypeEnum.Endclosingdate, currentService);
            valuesDto.WaitTime = GetFormValue<int?>(ExtraFieldSystemTypeEnum.Waittime, currentService);
            valuesDto.Kilometers = GetFormValue<double?>(ExtraFieldSystemTypeEnum.Kilometers, currentService);
            valuesDto.TravelTime = GetFormValue<int?>(ExtraFieldSystemTypeEnum.Traveltime, currentService);

            if (valuesDto.StartDate != null && valuesDto.EndDate != null)
            {
                valuesDto.FinalInitDate = valuesDto.StartDate;
                valuesDto.FinalEndDate = valuesDto.EndDate;
                valuesDto.FinalWaitTime = valuesDto.WaitTime;
                valuesDto.FinalIsIntervention = true;
            }
            else if (valuesDto.StartClosingDate != null && valuesDto.EndClosingDate != null)
            {
                valuesDto.FinalInitDate = valuesDto.StartClosingDate;
                valuesDto.FinalEndDate = valuesDto.EndClosingDate;
                valuesDto.FinalWaitTime = valuesDto.WaitTime;
                valuesDto.FinalIsIntervention = false;
            }

            return valuesDto;
        }

        private void AddServiceToAnalyze(WorkOrders currentWorkOrder, Services currentService, Entities.Tenant.People currentPeople)
        {
            var analysisFormDates = GetAnalysisFormDates(currentService);

            if (analysisFormDates.EndingTime != DateTime.MinValue && analysisFormDates.StartTime != DateTime.MinValue && analysisFormDates.StartTime < analysisFormDates.EndingTime)
            {
                var analysisFormValues = GetAnalysisFormValues(currentService, currentPeople, analysisFormDates.StartTime, analysisFormDates.EndingTime);

                var newAnalysis = new ServicesAnalysis
                {
                    Service = currentService,
                    WorkOrderCodeNavigation = currentWorkOrder,
                    WorkOrderCode = currentService.WorkOrderId,
                    CreationDateTime = DateTime.UtcNow,
                    Technician = currentService.PeopleResponsibleId.GetValueOrDefault(),
                    DeliveryNote = null,
                    Observacions = currentService.Observations,
                    StartTime = analysisFormDates.StartTime,
                    EndingTime = analysisFormDates.EndingTime,
                    ServiceDescription = analysisFormValues.Description,
                    TravelTime = analysisFormValues.TravelTime,
                    Kilometers = (decimal?)analysisFormValues.Kilometers,
                    WaitTime = analysisFormValues.WaitTime,
                    WorkedTime = analysisFormValues.Worked,
                    OnSiteTime = analysisFormValues.OnSite,
                    StandardPersonCost = analysisFormValues.PersonCost,
                    ProductionCost = analysisFormValues.WorkedCost,
                    TravelTimeCost = analysisFormValues.TravelCost,
                    KmCost = analysisFormValues.KmCost,
                    SubcontractorCode = analysisFormValues.SubCode,
                    SubcontractorName = analysisFormValues.SubName,
                    SubcontractorCost = (decimal?)analysisFormValues.SubContractCost
                };

                if (currentService.ClosingCodeId != null)
                {
                    var closingCodesAnalysis = _closingCodeService.GetAnalyzeClosingCodesById(currentService.ClosingCodeId.Value);
                    newAnalysis.ClosingCodeDesc1 = closingCodesAnalysis.ClosingCodeDesc1;
                    newAnalysis.ClosingCodeName1 = closingCodesAnalysis.ClosingCodeName1;
                    newAnalysis.ClosingCodeDesc2 = closingCodesAnalysis.ClosingCodeDesc2;
                    newAnalysis.ClosingCodeName2 = closingCodesAnalysis.ClosingCodeName2;
                    newAnalysis.ClosingCodeDesc3 = closingCodesAnalysis.ClosingCodeDesc3;
                    newAnalysis.ClosingCodeName3 = closingCodesAnalysis.ClosingCodeName3;
                    newAnalysis.ClosingCodeDesc4 = closingCodesAnalysis.ClosingCodeDesc4;
                    newAnalysis.ClosingCodeName4 = closingCodesAnalysis.ClosingCodeName4;
                    newAnalysis.ClosingCodeDesc5 = closingCodesAnalysis.ClosingCodeDesc5;
                    newAnalysis.ClosingCodeName5 = closingCodesAnalysis.ClosingCodeName5;
                    newAnalysis.ClosingCodeDesc6 = closingCodesAnalysis.ClosingCodeDesc6;
                    newAnalysis.ClosingCodeName6 = closingCodesAnalysis.ClosingCodeName6;
                }

                currentWorkOrder.ServicesAnalysis.Add(newAnalysis);
            }
        }

        private void UpdateServiceToAnalyze(Services currentService, ServicesAnalysis currentAnalysis, Entities.Tenant.People currentPeople)
        {
            var analysisFormDates = GetAnalysisFormDates(currentService);

            if (analysisFormDates.EndingTime != DateTime.MinValue && analysisFormDates.StartTime != DateTime.MinValue && analysisFormDates.StartTime < analysisFormDates.EndingTime)
            {
                var analysisFormValues = GetAnalysisFormValues(currentService, currentPeople, analysisFormDates.StartTime, analysisFormDates.EndingTime);

                currentAnalysis.Observacions = currentService.Observations;
                currentAnalysis.StartTime = analysisFormDates.StartTime;
                currentAnalysis.EndingTime = analysisFormDates.EndingTime;
                currentAnalysis.ServiceDescription = analysisFormValues.Description;
                currentAnalysis.TravelTime = analysisFormValues.TravelTime;
                currentAnalysis.Kilometers = (decimal?)analysisFormValues.Kilometers;
                currentAnalysis.WaitTime = analysisFormValues.WaitTime ?? 0;
                currentAnalysis.WorkedTime = analysisFormValues.Worked;
                currentAnalysis.OnSiteTime = analysisFormValues.OnSite;
                currentAnalysis.StandardPersonCost = analysisFormValues.PersonCost;
                currentAnalysis.ProductionCost = analysisFormValues.WorkedCost;
                currentAnalysis.TravelTimeCost = analysisFormValues.TravelCost;
                currentAnalysis.KmCost = analysisFormValues.KmCost;
                currentAnalysis.SubcontractorCode = analysisFormValues.SubCode;
                currentAnalysis.SubcontractorName = analysisFormValues.SubName;
                currentAnalysis.SubcontractorCost = (decimal?) analysisFormValues.SubContractCost;
            }
        }

        private AnalysisFormDatesDto GetAnalysisFormDates(Services currentService)
        {
            var startDate = GetFormValue<DateTime?>(ExtraFieldSystemTypeEnum.Startdate, currentService);
            var endDate = GetFormValue<DateTime?>(ExtraFieldSystemTypeEnum.Enddate, currentService);
            var startClosingDate = GetFormValue<DateTime?>(ExtraFieldSystemTypeEnum.Startclosingdate, currentService);
            var endClosingDate = GetFormValue<DateTime?>(ExtraFieldSystemTypeEnum.Endclosingdate, currentService);

            var analysisFormDates = new AnalysisFormDatesDto
            {
                StartTime = startDate ?? startClosingDate ?? DateTime.MinValue,
                EndingTime = endDate ?? endClosingDate ?? DateTime.MinValue
            };

            return analysisFormDates;
        }

        private AnalysisFormValuesDto GetAnalysisFormValues(Services currentService, Entities.Tenant.People currentPeople, DateTime startTime, DateTime endingTime)
        {
            var analysisFormValues = new AnalysisFormValuesDto();

            analysisFormValues.TravelTime = GetFormValue<int?>(ExtraFieldSystemTypeEnum.Traveltime, currentService) ?? 0;
            analysisFormValues.WaitTime = GetFormValue<int?>(ExtraFieldSystemTypeEnum.Waittime, currentService) ?? 0;
            analysisFormValues.Kilometers = GetFormValue<double?>(ExtraFieldSystemTypeEnum.Kilometers, currentService) ?? 0;
            analysisFormValues.Description = GetFormValue<string>(ExtraFieldSystemTypeEnum.Description, currentService);
            analysisFormValues.Worked = Convert.ToInt32((endingTime - startTime).TotalMinutes);
            analysisFormValues.OnSite = analysisFormValues.Worked - analysisFormValues.WaitTime;

            analysisFormValues.PersonCost = currentPeople.PeopleCost?.Where(pc => pc.StartDate <= startTime && (pc.EndDate >= endingTime || pc.EndDate == null)).Select(p => p.HourCost).FirstOrDefault();
            analysisFormValues.WorkedCost = analysisFormValues.PersonCost * analysisFormValues.OnSite / 60;
            analysisFormValues.TravelCost = analysisFormValues.PersonCost * analysisFormValues.TravelTime / 60;
            analysisFormValues.KmCost = (currentService.PeopleResponsible.IsClientWorker == 1 &&
                          !currentService.PeopleResponsible.CostKm.HasValue)
                ? (decimal?)currentService.PeopleResponsible.Company?.CostKm
                : (decimal?)currentService.PeopleResponsible.CostKm;

            analysisFormValues.SubCode = currentPeople.Subcontract?.Id ?? 0;
            analysisFormValues.SubName = currentPeople.Subcontract?.Name;

            analysisFormValues.SubContractCost = currentService.Bill?.SelectMany(b => b.BillLine).SelectMany(bl => bl.Item.ItemsPurchaseRate).Sum(ipr => ipr.Price) ?? 0;

            return analysisFormValues;
        }

        public T GetFormValue<T>(ExtraFieldSystemTypeEnum extraFieldType, Services currentService)
        {
            var value = default(T);
            object fieldValue = null;
            int efId;
            switch (extraFieldType)
            {
                case ExtraFieldSystemTypeEnum.Description:
                    efId = _extraFieldsService.GetExtraFieldIdFormName(ExtraFieldsConstants.ExtraFieldSystemTypeDescription);
                    fieldValue = currentService.ExtraFieldsValues.FirstOrDefault(efv => efv.ExtraFieldId == efId)?.StringValue;
                    break;
                case ExtraFieldSystemTypeEnum.Kilometers:
                    efId = _extraFieldsService.GetExtraFieldIdFormName(ExtraFieldsConstants.ExtraFieldSystemTypeKm);
                    fieldValue = currentService.ExtraFieldsValues.FirstOrDefault(efv => efv.ExtraFieldId == efId)?.DecimalValue;
                    break;
                case ExtraFieldSystemTypeEnum.Traveltime:
                    efId = _extraFieldsService.GetExtraFieldIdFormName(ExtraFieldsConstants.ExtraFieldSystemTypeTravelTime);
                    fieldValue = currentService.ExtraFieldsValues.FirstOrDefault(efv => efv.ExtraFieldId == efId)?.EnterValue;
                    break;
                case ExtraFieldSystemTypeEnum.Waittime:
                    efId = _extraFieldsService.GetExtraFieldIdFormName(ExtraFieldsConstants.ExtraFieldSystemTypeWaitTime);
                    fieldValue = currentService.ExtraFieldsValues.FirstOrDefault(efv => efv.ExtraFieldId == efId)?.EnterValue;
                    break;
                case ExtraFieldSystemTypeEnum.Startdate:
                    efId = _extraFieldsService.GetExtraFieldIdFormName(ExtraFieldsConstants.ExtraFieldSystemTypeStartDate);
                    fieldValue = currentService.ExtraFieldsValues.FirstOrDefault(efv => efv.ExtraFieldId == efId)?.DataValue;
                    break;
                case ExtraFieldSystemTypeEnum.Enddate:
                    efId = _extraFieldsService.GetExtraFieldIdFormName(ExtraFieldsConstants.ExtraFieldSystemTypeEndDate);
                    fieldValue = currentService.ExtraFieldsValues.FirstOrDefault(efv => efv.ExtraFieldId == efId)?.DataValue;
                    break;
                case ExtraFieldSystemTypeEnum.Startclosingdate:
                    efId = _extraFieldsService.GetExtraFieldIdFormName(ExtraFieldsConstants.ExtraFieldSystemTypeStartClosingDate);
                    fieldValue = currentService.ExtraFieldsValues.FirstOrDefault(efv => efv.ExtraFieldId == efId)?.DataValue;
                    break;
                case ExtraFieldSystemTypeEnum.Endclosingdate:
                    efId = _extraFieldsService.GetExtraFieldIdFormName(ExtraFieldsConstants.ExtraFieldSystemTypeEndClosingDate);
                    fieldValue = currentService.ExtraFieldsValues.FirstOrDefault(efv => efv.ExtraFieldId == efId)?.DataValue;
                    break;
            }

            value = (T) fieldValue;

            return value;
        }
    }
}
