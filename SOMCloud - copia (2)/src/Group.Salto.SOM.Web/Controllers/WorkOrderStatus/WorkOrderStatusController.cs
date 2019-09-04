using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.WorkOrderStatus;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Language;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderStatus;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.WorkOrderStatus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.SOM.Web.Controllers.WorkOrderStatus
{
    public class WorkOrderStatusController : BaseLanguageController
    {
        private readonly IWorkOrderStatusService _workOrderStatusService;

        public WorkOrderStatusController(ILoggingService loggingService,
                                            IConfiguration configuration,
                                            IHttpContextAccessor accessor,
                                            IWorkOrderStatusService workOrderStatusService,
                                            ILanguageService languageService) : base(loggingService, configuration, accessor, languageService)
        {
            _workOrderStatusService = workOrderStatusService ?? throw new ArgumentNullException($"{nameof(IWorkOrderStatusService)} is null");
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.WOStatus, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"WorkOrderStatusController.Get get all work status");
            var result = DoFilterAndPaging(new WorkOrderStatusFilterViewModel());
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.WOStatus, ActionEnum.Create)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.WOStatus, ActionEnum.GetById)]
        public IActionResult Edit(int id)
        {
            LoggingService.LogInfo($"WorkOrderStatus.Get WorkOrderStatus for id:{id}");
            var result = _workOrderStatusService.GetById(id);
            var languages = LanguageService.GetAll()?.Data;
            var response = result.ToResultViewModel(languages);
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

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.WOStatus, ActionEnum.Update)]
        public IActionResult Edit(WorkOrderStatusViewModel model)
        {
            LoggingService.LogInfo($"WorkOrderStatus update for id ={model.Id}");
            if (ModelState.IsValid)
            {
                var result = _workOrderStatusService.Update(model.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                        WorkOrderStatusConstants.WorkOrderStatusSuccessUpdateMessage,
                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToResultViewModel(LanguageService.GetAll()?.Data);
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Edit", resultData);
            }
            return ModelInvalid("Edit", model);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.WOStatus, ActionEnum.Create)]
        public IActionResult Create(WorkOrderStatusViewModel model)
        {
            LoggingService.LogInfo("WorkOrderStatus create");
            if (ModelState.IsValid)
            {
                var result = _workOrderStatusService.Create(model.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                        WorkOrderStatusConstants.WorkOrderStatusSuccessCreateMessage,
                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToResultViewModel(LanguageService.GetAll()?.Data);
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Create", resultData);
            }
            return ModelInvalid("Create", model);
        }

        [HttpPost]
        public IActionResult Filter(WorkOrderStatusFilterViewModel filter)
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

        [HttpDelete]
        [CustomAuthorization(ActionGroupEnum.WOStatus, ActionEnum.Delete)]
        public bool Delete(int id)
        {
            var deleteResult = _workOrderStatusService.Delete(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    WorkOrderStatusConstants.WorkOrderStatusDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                    WorkOrderStatusConstants.WorkOrderStatusDeleteErrorMessage,
                    FeedbackTypeEnum.Error);
            }
            return deleteResult.Data;
        }

        private ResultViewModel<WorkOrderStatusesListViewModel> DoFilterAndPaging(WorkOrderStatusFilterViewModel filter)
        {
            var data = new ResultViewModel<WorkOrderStatusesListViewModel>();
            filter.LanguageId = GetLanguageId();
            var filteredWorkStatus = _workOrderStatusService.GetAllFilteredByLanguage(filter.ToDto()).Data.ToViewModel();
            if (filter.Page != 0)
            {
                filter.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<WorkOrderStatusListViewModel>()
                {
                    Page = filter.Page,
                    PageSize = filter.Size
                };
                data.Data = new WorkOrderStatusesListViewModel()
                {
                    WorkOrderStatusList = new MultiItemViewModel<WorkOrderStatusListViewModel, int>(pager.GetPageIEnumerable(filteredWorkStatus))
                };
                filter.PagesCount = pager.PagesCount;
                filter.Page = pager.Page;
                filter.TotalValues = filteredWorkStatus.Count();
            }
            else
            {
                data.Data = new WorkOrderStatusesListViewModel()
                {
                    WorkOrderStatusList = new MultiItemViewModel<WorkOrderStatusListViewModel, int>(filteredWorkStatus)
                };
            }
            data.Data.WorkOrderStatusFilter = filter;
            return data;
        }

        private IActionResult ModelInvalid(string view, WorkOrderStatusViewModel workOrderStatus)
        {
            return View(view, ProcessResult(workOrderStatus, new List<FeedbackViewModel>()
            {
                new FeedbackViewModel()
                {
                    TitleKey = LocalizationsConstants.Error,
                    MessageKey = LocalizationsConstants.FormErrorsMessage,
                    FeedbackType = FeedbackTypeEnum.Error,
                }
            }));
        }
    }
}