using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.Actions;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Actions;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Actions;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.SOM.Web.Controllers.Actions
{
    public class ActionsController : BaseController
    {
        private readonly IActionService _actionService;

        public ActionsController(
            ILoggingService loggingService,
            IHttpContextAccessor accessor,
            IConfiguration configuration,
            IActionService actionService) : base(loggingService, configuration, accessor)
        {
            _actionService = actionService ?? throw new ArgumentNullException($"{nameof(actionService)} is null");
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Actions, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"Actions.Get get all actions");
            var result = DoFilterAndPaging(new ActionsFilterViewModel());
            result.Breadcrumbs = AddBreadcrumb(new List<string>() { MenuLocalizationConstants.GeneralMenuTitleSystem, ActionsConstants.ActionsPageTitle });

            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Actions, ActionEnum.GetById)]
        public IActionResult Details(int id)
        {
            LoggingService.LogInfo($"ActionController.Get detail action for id = {id}");
            var result = _actionService.GetById(id);
            var resultData = result.ToViewModel();
            resultData.Breadcrumbs = AddBreadcrumb(new List<string>() { MenuLocalizationConstants.GeneralMenuTitleSystem, ActionsConstants.ActionsPageTitle, ActionsConstants.ActionsDetailsTitle });

            if (result != null && (result.Errors?.Errors == null || !result.Errors.Errors.Any()))
            {
                return View("Details", resultData);
            }

            return View();
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Actions, ActionEnum.Update)]
        public IActionResult Update(ResultViewModel<ActionViewModel> model)
        {
            LoggingService.LogInfo($"ActionController.Post Update action");
            if (ModelState.IsValid)
            {
                var resultUpdate = _actionService.UpdateAction(model.Data.ToDto());
                if (resultUpdate.Errors?.Errors == null || !resultUpdate.Errors.Errors.Any())
                {
                    LoggingService.LogInfo($"ActionController.Post Updated action with id ={model.Data.Id}");
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        ActionsConstants.ActionsUpdateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = resultUpdate.ToViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Details", resultData);
            }
            var result = ProcessResult(model.Data, LocalizationsConstants.Error,
                                        LocalizationsConstants.FormErrorsMessage,
                                        FeedbackTypeEnum.Error);
            return View("Details", result);
        }

        [HttpPost]
        public IActionResult Filter(ActionsFilterViewModel filter)
        {
            var resultData = DoFilterAndPaging(filter);
            return View(nameof(Index), resultData);
        }

        private ResultViewModel<ActionsViewModel> DoFilterAndPaging(ActionsFilterViewModel filters)
        {
            var data = new ResultViewModel<ActionsViewModel>();
            var filteredData = _actionService.GetAllFiltered(filters.ToDto()).Data.ToViewModel();
            if (filters.Page != 0)
            {
                filters.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<ActionViewModel>()
                {
                    Page = filters.Page,
                    PageSize = filters.Size
                };
                data.Data = new ActionsViewModel()
                {
                    Actions = new MultiItemViewModel<ActionViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filters.PagesCount = pager.PagesCount;
                filters.Page = pager.Page;
                filters.TotalValues = filteredData.Count();
            }
            else
            {
                data.Data = new ActionsViewModel()
                {
                    Actions = new MultiItemViewModel<ActionViewModel, int>(filteredData)
                };
            }
            data.Data.ActionsFilters = filters;
            return data;
        }
    }
}