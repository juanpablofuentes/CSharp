using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.Expense;
using Group.Salto.Common.Constants.ExpenseTicket;
using Group.Salto.Common.Dictionaries;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Expense;
using Group.Salto.ServiceLibrary.Common.Contracts.ExpenseTicket;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.File;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Expense;
using Group.Salto.SOM.Web.Models.ExpenseTicket;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.MultiCombo;
using Group.Salto.SOM.Web.Models.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Controllers.ExpenseTicket
{
    public class ExpenseTicketController : BaseController
    {
        private readonly IExpenseService _expenseService;

        public ExpenseTicketController(ILoggingService loggingService,
            IConfiguration configuration,
            IHttpContextAccessor accessor,
            IExpenseService expenseService)
            : base(loggingService, configuration, accessor)
        {
            _expenseService = expenseService ?? throw new ArgumentNullException(nameof(IExpenseService));
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.ExpensesReport, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"Actions Get get all Expense tickets");
            var model = new ExpenseTicketFilterViewModel();
            model = FillDataStates(model);
            var result = DoFilterAndPaging(model);
            result.Breadcrumbs = AddBreadcrumb(new List<string>() { MenuLocalizationConstants.GeneralMenuTitleExpenseReport, MenuLocalizationConstants.GeneralSubmenuExpenseReportDeliveryNotes });
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.ExpensesReport, ActionEnum.Create)]
        public IActionResult Create()
        {
            LoggingService.LogInfo("Return Expense Create");
            var response = new ResultViewModel<ExpenseDetailsViewModel>()
            {
                Data = FillDataSelectList(new ExpenseDetailsViewModel(), ModeActionTypeEnum.Create)

            };
            response.Breadcrumbs = AddBreadcrumb(new List<string>() { MenuLocalizationConstants.GeneralMenuTitleExpenseReport, MenuLocalizationConstants.GeneralSubmenuExpenseReportDeliveryNotes, ExpenseConstants.ExpenseCreateTitle });
            response.Data.FileExists = false;

            return View(response);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.ExpensesReport, ActionEnum.GetById)]
        public IActionResult Edit(int id)
        {
            LoggingService.LogInfo($"Expenses .get for id:{id}");
            var result = _expenseService.GetByIdWithExpenseAndFile(id);
            var response = result.ToViewModel();
            var status = _expenseService.GetExpenseTicketStatus();
            response.Breadcrumbs = AddBreadcrumb(new List<string>() { MenuLocalizationConstants.GeneralMenuTitleExpenseReport, MenuLocalizationConstants.GeneralSubmenuExpenseReportDeliveryNotes, ExpenseConstants.ExpenseDetailsTitle });
            response.Data.StatusGuids = status;
            response.Data.FileExists = CheckFile(id);
            FillDataSelectList(response.Data, ModeActionTypeEnum.Edit);
            LogFeedbacks(response.Feedbacks?.Feedbacks);
            if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
            {
                return View("Edit", response);
            }
            SetFeedbackTempData(LocalizationsConstants.Error,
                LocalizationsConstants.ErrorLoadingDataMessage,
                FeedbackTypeEnum.Error);
            return Redirect(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            LoggingService.LogInfo($"Expenses .get for id:{id}");
            var result = _expenseService.GetByIdWithExpenseAndFile(id);
            var response = result.ToViewModel();
            var status = _expenseService.GetExpenseTicketStatus();
            response.Breadcrumbs = AddBreadcrumb(new List<string>() { MenuLocalizationConstants.GeneralMenuTitleExpenseReport, MenuLocalizationConstants.GeneralSubmenuExpenseReportDeliveryNotes, ExpenseConstants.ExpenseDetailsTitle });
            response.Data.StatusGuids = status;
            response.Data.FileExists = CheckFile(id);
            FillDataSelectList(response.Data, ModeActionTypeEnum.List);
            LogFeedbacks(response.Feedbacks?.Feedbacks);
            if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
            {
                return View("Details", response);
            }
            SetFeedbackTempData(LocalizationsConstants.Error,
                LocalizationsConstants.ErrorLoadingDataMessage,
                FeedbackTypeEnum.Error);
            return Redirect(nameof(Index));
        }

        [HttpGet("Download")]
        public IActionResult Download(int id)
        {
            var file = _expenseService.GetFileFromExpense(id);
            if (file != null && file.FileBytes != null && file.FileName != null)
            {
                return File(file.FileBytes, MimeTypesDictionary.GetMimeType(Path.GetExtension(file.FileName)), file.FileName);
            }
            SetFeedbackTempData(LocalizationsConstants.Error,
                LocalizationsConstants.ErrorLoadingDataMessage,
                FeedbackTypeEnum.Error);
            return RedirectToAction("Details", new { @id = id });

        }

        [HttpPost]
        public IActionResult Filter(ExpenseTicketFilterViewModel filter)
        {
            var resultData = DoFilterAndPaging(filter);
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                resultData.Feedbacks = resultData.Feedbacks ?? new FeedbacksViewModel();
                resultData.Feedbacks.AddFeedback(feedback);
            }
            return View(nameof(Index), resultData);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.ExpensesReport, ActionEnum.Create)]
        public IActionResult Create(ExpenseDetailsViewModel expenseData)
        {
            LoggingService.LogInfo($"Actions Post create Expense");
            if (ModelState.IsValid)
            {
                var peopleConfigId = GetConfigurationUserId();
                var result = _expenseService.CreateExpense(expenseData.ToExtDto(), peopleConfigId);
                if (expenseData.UploadTicket != null)
                {
                    var fileData = new RequestFileDto();
                    fileData.Id = result.Data.Id;
                    fileData.FileName = expenseData.UploadTicket.FileName;
                    using (var ms = new MemoryStream())
                    {
                        expenseData.UploadTicket.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        fileData.FileBytes = fileBytes;
                    }
                    var resultFile = _expenseService.AddFileToExpense(fileData);
                    if (resultFile.Errors?.Errors == null || !resultFile.Errors.Errors.Any())
                    {
                        SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                            ExpenseConstants.ExpenseCreateSuccessMessage,
                                            FeedbackTypeEnum.Success);
                        return RedirectToAction("Index");
                    }
                }
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        ExpenseConstants.ExpenseCreateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }

                var resultData = result.ToViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                FillDataSelectList(resultData.Data, ModeActionTypeEnum.Create);
                return View("Create", resultData);
            }
            var results = ProcessResult(expenseData,
                         LocalizationsConstants.Error,
                         LocalizationsConstants.FormErrorsMessage,
                         FeedbackTypeEnum.Error);
            FillDataSelectList(expenseData, ModeActionTypeEnum.Create);
            return View("Create", results);
        }

        [HttpPost]
        public IActionResult Edit(ExpenseDetailsViewModel expenseData)
        {
            LoggingService.LogInfo($"Actions Post Update expense by id {expenseData.Id}");
            var status = _expenseService.GetExpenseTicketStatus();
            if (ModelState.IsValid)
            {
                var resultUpdate = _expenseService.UpdateExpense(expenseData.ToExtDto());
                if (resultUpdate.Errors?.Errors == null || !resultUpdate.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        ExpenseConstants.ExpenseUpdateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }


                var resultData = resultUpdate.ToViewModel();
                resultData.Data.StatusGuids = status;
                resultData.Data.FileExists = CheckFile(expenseData.ExpenseTicketId);
                FillDataSelectList(resultData.Data, ModeActionTypeEnum.Edit);
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Edit", resultData);
            }

            expenseData.StatusGuids = status;
            expenseData.FileExists = CheckFile(expenseData.ExpenseTicketId);
            FillDataSelectList(expenseData, ModeActionTypeEnum.Edit);
            var result = ProcessResult(expenseData, LocalizationsConstants.Error, LocalizationsConstants.FormErrorsMessage, FeedbackTypeEnum.Error);
            return View("Edit", result);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.ExpensesReport, ActionEnum.Delete)]
        public IActionResult Delete(int id)
        {
            LoggingService.LogInfo($"Actions Post Delete Expense by id {id}");
            var deleteResult = _expenseService.DeleteExpense(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    ExpenseConstants.ExpenseDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
                return Ok(deleteResult.Data);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                                    ExpenseConstants.ExpenseDeleteErrorMessage,
                                    FeedbackTypeEnum.Error);
                return BadRequest(deleteResult.Data);
            }
        }

        [HttpGet("GetExpense")]
        public IActionResult GetExpense(int id, string status)
        {
            var result = _expenseService.GetByIdWithExpenseAndFile(id);
            if (result == null)
            {
                return NotFound("Not data found");
            }
            var statusList = status.Split("|");
            var expenseModel = result.Data.ToViewModel();
            PartialViewResult resultModel = new PartialViewResult();
            resultModel = AssignValuesToPartialView(resultModel, statusList);
            resultModel.ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
            {
                Model = expenseModel
            };
            return resultModel;
        }

        [HttpPost]
        public IActionResult ValidateExpense(ExpenseDetailsViewModel model)
        {
            var result = _expenseService.GetByIdWithExpenseAndFile(model.Id);
            var status = _expenseService.GetExpenseTicketStatus();
            var peopleConfigId = GetConfigurationUserId();
            result.Data = model.ToValidateDto(result.Data, peopleConfigId);
            LoggingService.LogInfo($"Actions Post validate Expense");
            if (result != null && status != null && model.ExpenseStatusId != null && status.Any(x => x.Name.Contains(model.NextStatus)))
            {
                var resultFile = _expenseService.ValidateExpense(result.Data, status.FirstOrDefault(x => x.Name.Contains(model.NextStatus)).Id);
                if (resultFile.Errors?.Errors == null || !resultFile.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        ExpenseConstants.ExpenseValidationSuccess,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                else
                {
                    SetFeedbackTempData(LocalizationsConstants.Error,
                                        ExpenseConstants.ExpenseValidationError,
                                        FeedbackTypeEnum.Error);
                    return RedirectToAction("Index", resultFile.Data);
                }
            }
            else
            {
                var final = result.ToViewModel();
                final.Data.StatusGuids = status;

                SetFeedbackTempData(LocalizationsConstants.Error,
                                    ExpenseConstants.ExpenseValidationError,
                                    FeedbackTypeEnum.Error);
                return RedirectToAction("Index", final);
            }
        }

        private bool CheckFile(int id)
        {
            var file = _expenseService.GetFileFromExpense(id);
            if (file != null && file.FileName != null && file.FileName != null)
            {
                return true;
            }
            else return false;
        }

        private ExpenseTicketFilterViewModel FillDataStates(ExpenseTicketFilterViewModel source)
        {
            var types = _expenseService.GetDefaultStates();
            source.States = new List<MultiComboViewModel<Guid?, string>>();
            foreach (var item in types)
            {
                var ele = new MultiComboViewModel<Guid?, string>();
                ele.Value = item.Key;
                ele.Text = item.Value;
                source.States?.Add(ele);
            }
            return source;
        }

        private ExpenseDetailsViewModel FillDataSelectList(ExpenseDetailsViewModel source, ModeActionTypeEnum ModeAction)
        {
            source.ModeActionType = ModeAction;
            source.PaymentMethodsList = _expenseService.GetPaymentMethodKeyValues()?.ToSelectList();
            source.ExpenseTypesList = _expenseService.GetExpenseTypeKeyValues()?.ToSelectList();
            return source;
        }

        private ResultViewModel<ExpensesTicketsViewModel> DoFilterAndPaging(ExpenseTicketFilterViewModel filters)
        {
            
            var data = new ResultViewModel<ExpensesTicketsViewModel>();
            var filteredData = _expenseService.GetAllFiltered(filters.ToDto());
            filteredData.Data = _expenseService.CalculateAmount(filteredData.Data);
            var expenseData = _expenseService.GetAllExpenseFiltered(filters.ToDto());
            var response = expenseData.Data.ToListViewModel();
            var resultData = filteredData.Data.ToViewModel();
            filters.Size = GetNumberEntriesPerPage();

            var count = _expenseService.CountId(filters.ToDto());

            if (resultData.Count < filters.Size - 1)
            {
                count = resultData.Count;
            }
            if (filters.Page != 0)
            {
                
                var pager = new PagedSelector<ExpenseTicketViewModel>()
                {
                    Page = filters.Page,
                    PageSize = filters.Size
                };
                data.Data = new ExpensesTicketsViewModel()
                {
                    ExpenseTickets = new MultiItemViewModel<ExpenseTicketViewModel, int>(resultData),
                    ExpenseModal = new MultiItemViewModel<ExpenseDetailsViewModel, int>(response)
                };
                filters.PagesCount = (int)Math.Ceiling((double)count / (double)pager.PageSize);
                filters.Page = pager.Page;
                filters.TotalValues = resultData.Count();
            }
            else
            {
                data.Data = new ExpensesTicketsViewModel()
                {
                    ExpenseTickets = new MultiItemViewModel<ExpenseTicketViewModel, int>(resultData),
                    ExpenseModal = new MultiItemViewModel<ExpenseDetailsViewModel, int>(response)
                };
            }

            data.Data.ExpenseTicketFilters = filters;
            return data;
        }

        private PartialViewResult AssignValuesToPartialView(PartialViewResult modelView, string[] status)
        {
            if (String.Compare(status[0], ExpenseTicketConstants.ExpenseTicketAccepted) == 0)
            {
                if (String.Compare(status[1], "PaidModal") == 0)
                {
                    modelView.ViewName = "_PaidModal";
                }
                else if (String.Compare(status[1], "FinishedModal") == 0)
                {
                    modelView.ViewName = "_FinishedModal";
                }
            }

            else if (String.Compare(status[0], ExpenseTicketConstants.ExpenseTicketEscaled) == 0)
            {
                if (String.Compare(status[1], "AcceptedModal") == 0)
                {
                    modelView.ViewName = "_AcceptedModal";
                }
                else if (String.Compare(status[1], "RejectedModal") == 0)
                {
                    modelView.ViewName = "_RejectedModal";
                }
            }
            else if (String.Compare(status[0], ExpenseTicketConstants.ExpenseTicketPending) == 0)
            {
                if (String.Compare(status[1], "AcceptedModal") == 0)
                {
                    modelView.ViewName = "_AcceptedModal";
                }
                else if (String.Compare(status[1], "RejectedModal") == 0)
                {
                    modelView.ViewName = "_RejectedModal";
                }
            }
            return modelView;
        }
    }
}