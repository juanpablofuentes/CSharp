using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.Queue;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Language;
using Group.Salto.ServiceLibrary.Common.Contracts.Permisions;
using Group.Salto.ServiceLibrary.Common.Contracts.Queue;
using Group.Salto.ServiceLibrary.Common.Dtos.Queue;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Queue;
using Group.Salto.SOM.Web.Models.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.SOM.Web.Controllers.Queue
{
    public class QueueController : BaseLanguageController
    {
        private readonly IQueueService _queueService;
        private readonly IPermissionsService _permissionsService;

        public QueueController(ILoggingService loggingService,
            IConfiguration configuration,
            IHttpContextAccessor accessor,
            IQueueService queueService,
            IPermissionsService permissionsService,
            ILanguageService languageService) : base(loggingService, configuration, accessor, languageService)
        {
            _queueService = queueService ?? throw new ArgumentNullException($"{nameof(IQueueService)} is null");
            _permissionsService = permissionsService ?? throw new ArgumentNullException($"{nameof(IPermissionsService)} is null");
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Queue, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"QueueController.Get get all work status");
            var result = DoFilterAndPaging(new QueueFilterViewModel());
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Queue, ActionEnum.Create)]
        public IActionResult Create()
        {
            var result = new QueueListDto();
            var languages = LanguageService.GetAll()?.Data;
            var permissions = _permissionsService.GetAllKeyValues()?.ToList();
            var response = result.ToDetailViewModel(languages, permissions, LocalizationsConstants.Permissions);
            return View(ProcessResult(response));
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Queue, ActionEnum.GetById)]
        public IActionResult Edit(int id)
        {
            LoggingService.LogInfo($"queue.Get queue for id:{id}");
            var result = _queueService.GetById(id);
            var languages = LanguageService.GetAll()?.Data;
            var permissions = _permissionsService.GetAllKeyValues()?.ToList();
            var response = result.ToResultViewModel(languages, permissions, LocalizationsConstants.Permissions);
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
        [CustomAuthorization(ActionGroupEnum.Queue, ActionEnum.Update)]
        public IActionResult Edit(QueueViewModel model)
        {
            LoggingService.LogInfo($"queue update for id ={model.Id}");
            if (ModelState.IsValid)
            {
                var result = _queueService.Update(model.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                        QueueConstants.QueueSuccessUpdateMessage,
                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var permissions = _permissionsService.GetAllKeyValues()?.ToList();
                var resultData = result.ToResultViewModel(LanguageService.GetAll()?.Data, permissions, LocalizationsConstants.Permissions);
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Edit", resultData);
            }
            return ModelInvalid("Edit", model);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Queue, ActionEnum.Create)]
        public IActionResult Create(QueueViewModel model)
        {
            LoggingService.LogInfo("queue create");
            if (ModelState.IsValid)
            {
                var result = _queueService.Create(model.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                        QueueConstants.QueueSuccessCreateMessage,
                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var permissions = _permissionsService.GetAllKeyValues()?.ToList();
                var resultData = result.ToResultViewModel(LanguageService.GetAll()?.Data, permissions, LocalizationsConstants.Permissions); LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Create", resultData);
            }
            return ModelInvalid("Create", model);
        }

        [HttpPost]
        public IActionResult Filter(QueueFilterViewModel filter)
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
        [CustomAuthorization(ActionGroupEnum.Queue, ActionEnum.Delete)]
        public bool Delete(int id)
        {
            var deleteResult = _queueService.Delete(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    QueueConstants.QueueDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                    QueueConstants.QueueDeleteErrorMessage,
                    FeedbackTypeEnum.Error);
            }
            return deleteResult.Data;
        }

        private ResultViewModel<QueuesListViewModel> DoFilterAndPaging(QueueFilterViewModel filter)
        {
            var data = new ResultViewModel<QueuesListViewModel>();
            filter.LanguageId = GetLanguageId();
            var filteredWorkStatus = _queueService.GetAllFilteredByLanguage(filter.ToDto()).Data.ToViewModel();
            if (filter.Page != 0)
            {
                filter.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<QueueListViewModel>()
                {
                    Page = filter.Page,
                    PageSize = filter.Size
                };
                data.Data = new QueuesListViewModel()
                {
                    QueueList = new MultiItemViewModel<QueueListViewModel, int>(pager.GetPageIEnumerable(filteredWorkStatus))
                };
                filter.PagesCount = pager.PagesCount;
                filter.Page = pager.Page;
                filter.TotalValues = filteredWorkStatus.Count();
            }
            else
            {
                data.Data = new QueuesListViewModel()
                {
                    QueueList = new MultiItemViewModel<QueueListViewModel, int>(filteredWorkStatus)
                };
            }
            data.Data.QueueFilter = filter;
            return data;
        }

        private IActionResult ModelInvalid(string view, QueueViewModel queue)
        {
            return View(view, ProcessResult(queue, new List<FeedbackViewModel>()
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