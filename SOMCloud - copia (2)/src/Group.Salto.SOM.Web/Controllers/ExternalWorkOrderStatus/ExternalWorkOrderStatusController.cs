using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.ExternalWorkOrderStatus;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.ExternalWorkOrderStatus;
using Group.Salto.ServiceLibrary.Common.Contracts.Language;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.ExternalWorkOrderStatus;
using Group.Salto.SOM.Web.Models.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.SOM.Web.Controllers.ExternalWorkOrderStatus
{
    public class ExternalWorkOrderStatusController : BaseLanguageController
    {
        private readonly IExternalWorkOrderStatusService _externalWorkOrderStatusService;

        public ExternalWorkOrderStatusController(ILoggingService loggingService,
                                            IConfiguration configuration,
                                            IHttpContextAccessor accessor,
                                            IExternalWorkOrderStatusService externalWorkOrderStatusService,
                                            ILanguageService languageService) : base(loggingService, configuration, accessor, languageService)
        {
            _externalWorkOrderStatusService = externalWorkOrderStatusService ?? throw new ArgumentNullException($"{nameof(IExternalWorkOrderStatusService)} is null");
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.ExternalWOStatus, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"ExternalWorkOrderStatusController.Get get all extenral work status");
            var result = DoFilterAndPaging(new ExternalWorkOrderStatusFilterViewModel());
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.ExternalWOStatus, ActionEnum.Create)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.ExternalWOStatus, ActionEnum.GetById)]
        public IActionResult Edit(int id)
        {
            LoggingService.LogInfo($"ExternalWorkOrderStatus.Get ExternalWorkOrderStatus for id:{id}");
            var result = _externalWorkOrderStatusService.GetById(id);
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
        [CustomAuthorization(ActionGroupEnum.ExternalWOStatus, ActionEnum.Update)]
        public IActionResult Edit(ExternalWorkOrderStatusViewModel model)
        {
            LoggingService.LogInfo($"ExternalWorkOrderStatus update for id ={model.Id}");
            if (ModelState.IsValid)
            {
                var result = _externalWorkOrderStatusService.Update(model.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                        ExternalWorkOrderStatusConstants.ExternalWorkOrderStatusSuccessUpdateMessage,
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
        [CustomAuthorization(ActionGroupEnum.ExternalWOStatus, ActionEnum.Create)]
        public IActionResult Create(ExternalWorkOrderStatusViewModel model)
        {
            LoggingService.LogInfo("ExternalWorkOrderStatus create");
            if (ModelState.IsValid)
            {
                var result = _externalWorkOrderStatusService.Create(model.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                        ExternalWorkOrderStatusConstants.ExternalWorkOrderStatusSuccessCreateMessage,
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
        public IActionResult Filter(ExternalWorkOrderStatusFilterViewModel filter)
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
        [CustomAuthorization(ActionGroupEnum.ExternalWOStatus, ActionEnum.Delete)]
        public IActionResult Delete(int id)
        {
            var deleteResult = _externalWorkOrderStatusService.Delete(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    ExternalWorkOrderStatusConstants.ExternalWorkOrderStatusDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                    ExternalWorkOrderStatusConstants.ExternalWorkOrderStatusDeleteSuccessMessage,
                    FeedbackTypeEnum.Error);
            }
            return Ok(deleteResult.Data);
        }

        private IActionResult ModelInvalid(string view, ExternalWorkOrderStatusViewModel workOrderStatus)
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

        private ResultViewModel<ExternalWorkOrderStatusesListViewModel> DoFilterAndPaging(ExternalWorkOrderStatusFilterViewModel filter)
        {
            var data = new ResultViewModel<ExternalWorkOrderStatusesListViewModel>();
            filter.LanguageId = GetLanguageId();
            var filteredWorkStatus = _externalWorkOrderStatusService.GetAllFilteredByLanguage(filter.ToDto()).Data.ToViewModel();
            if (filter.Page != 0)
            {
                filter.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<ExternalWorkOrderStatusListViewModel>()
                {
                    Page = filter.Page,
                    PageSize = filter.Size
                };
                data.Data = new ExternalWorkOrderStatusesListViewModel()
                {
                    ExternalWorkOrderStatusList = new MultiItemViewModel<ExternalWorkOrderStatusListViewModel, int>(pager.GetPageIEnumerable(filteredWorkStatus))
                };
                filter.PagesCount = pager.PagesCount;
                filter.Page = pager.Page;
                filter.TotalValues = filteredWorkStatus.Count();
            }
            else
            {
                data.Data = new ExternalWorkOrderStatusesListViewModel()
                {
                    ExternalWorkOrderStatusList = new MultiItemViewModel<ExternalWorkOrderStatusListViewModel, int>(filteredWorkStatus)
                };
            }
            data.Data.ExternalWorkOrderStatusFilter = filter;
            return data;
        }
    }
}