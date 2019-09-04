using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.Flows;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Flows;
using Group.Salto.ServiceLibrary.Common.Contracts.Language;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Flows;
using Group.Salto.SOM.Web.Models.MultiSelect;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.Task;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.SOM.Web.Controllers.Flows
{
    public class FlowsController : BaseLanguageController
    {
        private readonly IFlowsService _flowsService;
        private readonly IConfiguration _appconfiguration;

        public FlowsController(ILoggingService loggingService,
            IConfiguration configuration,
            IHttpContextAccessor accessor,
            IFlowsService flowsService,
            ILanguageService languageService) : base(loggingService, configuration, accessor, languageService)
        {
            _flowsService = flowsService ?? throw new ArgumentNullException($"{nameof(IFlowsService)} is null");
            _appconfiguration = configuration ?? throw new ArgumentNullException($"{nameof(IConfiguration)} is null ");
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.WorkFlow, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"Flows.Get get all ");
            var result = DoFilterAndPaging(new FlowsFilterViewModel());
            result.Breadcrumbs = AddBreadcrumb(new List<string>() { MenuLocalizationConstants.GeneralSubmenuTitleConfiguration, FlowsConstants.FlowsPageTitle });

            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.WorkFlow, ActionEnum.Create)]
        public IActionResult Create()
        {
            LoggingService.LogInfo("Flows.Create");
            return View();
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.WorkFlow, ActionEnum.GetById)]
        public IActionResult Edit(int id)
        {
            LoggingService.LogInfo($"Flows.Edit for id:{id}");
            var languageId = this.GetLanguageId();
            var result = _flowsService.GetFlowsWithTasksInfoById(id, languageId);
            var response = result.ToDetailViewModel();
            response.Breadcrumbs = AddBreadcrumb(new List<string>() { MenuLocalizationConstants.GeneralSubmenuTitleConfiguration, FlowsConstants.FlowsPageTitle, FlowsConstants.FlowsEditPageTitle });
            //TODO Feedback
            //LogFeedbacks(response.Feedbacks?.Feedbacks);
            if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
            {
                MultiSelectViewModel multi = new MultiSelectViewModel("MultiSelectModal");
                TaskHeaderDetailViewModel editTask = new TaskHeaderDetailViewModel();
                MultiSelectViewModel multiModalTask = new MultiSelectViewModel("MultiSelectModalTask");
                editTask.ModalMultiselect = multiModalTask;
                response.Data.ModalMultiselect = multi;
                response.Data.FlowTaskEditViewModel = editTask;
                return View("Edit", response);
            }
            SetFeedbackTempData(LocalizationsConstants.Error,
                LocalizationsConstants.ErrorLoadingDataMessage,
                FeedbackTypeEnum.Error);
            return Redirect(nameof(Index));
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.WorkFlow, ActionEnum.Update)]
        public IActionResult Edit(FlowsViewModel model)
        {
            LoggingService.LogInfo($"Flows update for id ={model.Id}");
            if (ModelState.IsValid)
            {
                var result = _flowsService.Update(model.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                        FlowsConstants.FlowsSuccessUpdateMessage,
                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Edit", resultData);
            }
            return ModelInvalid("Edit", model.ToDetailViewModel());
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.WorkFlow, ActionEnum.Create)]
        public IActionResult Create(FlowsViewModel model)
        {
            LoggingService.LogInfo("queue create");
            if (ModelState.IsValid)
            {
                var result = _flowsService.Create(model.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                        FlowsConstants.FlowsSuccessCreateMessage,
                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToViewModel();
                return View("Create", resultData);
            }
            return ModelInvalid("Create", model.ToDetailViewModel());
        }

        [HttpPost]
        public IActionResult Filter(FlowsFilterViewModel filter)
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

        private IActionResult ModelInvalid(string view, FlowsDetailViewModel site)
        {
            return View(view, ProcessResult(FillData(site), new List<FeedbackViewModel>()
            {
                new FeedbackViewModel()
                {
                    TitleKey = LocalizationsConstants.Error,
                    MessageKey = LocalizationsConstants.FormErrorsMessage,
                    FeedbackType = FeedbackTypeEnum.Error,
                }
            }));
        }

        private FlowsDetailViewModel FillData(FlowsDetailViewModel source)
        {
            return source;
        }

        private ResultViewModel<FlowsListViewModel> DoFilterAndPaging(FlowsFilterViewModel filter)
        {
            var data = new ResultViewModel<FlowsListViewModel>();
            var filteredData = _flowsService.GetAllFiltered(filter.ToFilterDto()).Data.ToListViewModel();

            if (filter.Page != 0)
            {
                filter.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<FlowsViewModel>()
                {
                    Page = filter.Page,
                    PageSize = filter.Size
                };
                data.Data = new FlowsListViewModel()
                {
                    FlowsList = new MultiItemViewModel<FlowsViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filter.PagesCount = pager.PagesCount;
                filter.Page = pager.Page;
                filter.TotalValues = filteredData.Count;
            }
            else
            {
                data.Data = new FlowsListViewModel()
                {
                    FlowsList = new MultiItemViewModel<FlowsViewModel, int>(filteredData)
                };
            }
            data.Data.Filters = filter;
            return data;
        }
    }
}