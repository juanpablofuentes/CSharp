using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.EventCategories;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.AvailabilityCategories;
using Group.Salto.ServiceLibrary.Common.Contracts.EventCategories;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.EventCategories;
using Group.Salto.SOM.Web.Models.EventCategoriesViewModel;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.SOM.Web.Controllers.EventCategories
{
    public class EventCategoriesController : BaseController
    {
        private readonly IEventCategoriesService _eventCategoriesService;
        private readonly IAvailabilityCategoriesService _availabilityCategoriesService;

        public EventCategoriesController(ILoggingService loggingService,
            IConfiguration configuration,
            IHttpContextAccessor accessor,
            IEventCategoriesService eventCategoriesService,
            IAvailabilityCategoriesService availabilityCategoriesService) : base(loggingService, configuration, accessor)
        {
            _eventCategoriesService = eventCategoriesService ?? throw new ArgumentNullException(nameof(IEventCategoriesService));
            _availabilityCategoriesService = availabilityCategoriesService ?? throw new ArgumentNullException(nameof(IAvailabilityCategoriesService));
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.EventCategories, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"Event Categories -- Get get all work centers");
            var result = DoFilterAndPaging(new EventCategoriesFilterViewModel());
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.EventCategories, ActionEnum.GetById)]
        public IActionResult Details(int id)
        {
            LoggingService.LogInfo($"Event Categories -- detail id: {id}");
            var result = _eventCategoriesService.GetById(id);
            var resultData = result.ToDetailViewModel();

            FillData(resultData.Data);

            if (result != null && (result.Errors?.Errors == null || !result.Errors.Errors.Any()))
            {
                return View("Edit", resultData);
            }
            return View();
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.EventCategories, ActionEnum.Create)]
        public IActionResult Create()
        {
            LoggingService.LogInfo("Event Categories - Create Get");
            var response = new ResultViewModel<EventCategoriesDetailViewModel>()
            {
                Data = new EventCategoriesDetailViewModel()
            };

            FillData(response.Data);

            return View(response);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.EventCategories, ActionEnum.Create)]
        public IActionResult Create(EventCategoriesDetailViewModel createCategory)
        {
            LoggingService.LogInfo("Event Categories -- create");
            if (ModelState.IsValid)
            {
                var result = _eventCategoriesService.CreateEventCategories(createCategory.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                        EventCategoriesConstants.EventCategoryCreateSuccessMessage,
                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToDetailViewModel();
                FillData(resultData.Data);
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Create", resultData);
            }
            return ModelInvalid("Create", createCategory);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.EventCategories, ActionEnum.Update)]
        public IActionResult Edit(EventCategoriesDetailViewModel updateCategory)
        {
            LoggingService.LogInfo($"update Event Category by id {updateCategory.Id}");

            if (ModelState.IsValid)
            {
                var resultUpdate = _eventCategoriesService.UpdateEventCategories(updateCategory.ToDto());
                if (resultUpdate.Errors?.Errors == null || !resultUpdate.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        EventCategoriesConstants.EventCategoryUpdateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = resultUpdate.ToDetailViewModel();
                FillData(resultData.Data);
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Edit", resultData);
            }
            return ModelInvalid("Edit", updateCategory);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.EventCategories, ActionEnum.Delete)]
        public bool Delete(int id)
        {
            var deleteResult = _eventCategoriesService.DeleteEventCategories(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    EventCategoriesConstants.EventCategoryDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                                    EventCategoriesConstants.EventCategoryDeleteSuccessMessage,
                                    FeedbackTypeEnum.Error);
            }
            return deleteResult.Data;
        }        

        [HttpPost]
        public IActionResult Filter(EventCategoriesFilterViewModel filter)
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

        private ResultViewModel<EventsCategoriesListViewModel> DoFilterAndPaging(EventCategoriesFilterViewModel filters)
        {
            var data = new ResultViewModel<EventsCategoriesListViewModel>();
            var filteredData = _eventCategoriesService.GetAllFiltered(filters.ToFilterDto()).Data.ToViewModel();

            if (filters.Page != 0)
            {
                filters.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<EventCategoriesListViewModel>()
                {
                    Page = filters.Page,
                    PageSize = filters.Size
                };
                data.Data = new EventsCategoriesListViewModel()
                {
                    EventCategories = new MultiItemViewModel<EventCategoriesListViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filters.PagesCount = pager.PagesCount;
                filters.Page = pager.Page;
                filters.TotalValues = filteredData.Count();
            }
            else
            {
                data.Data = new EventsCategoriesListViewModel()
                {
                    EventCategories = new MultiItemViewModel<EventCategoriesListViewModel, int>(filteredData)
                };
            }

            data.Data.EventCategoriesFilter = filters;
            return data;
        }

        private IActionResult ModelInvalid(string view, EventCategoriesDetailViewModel results, string KeyMessage = null)
        {
            FillData(results);

            return View(view, ProcessResult(results, new List<FeedbackViewModel>()
            {
                new FeedbackViewModel()
                {
                    TitleKey = LocalizationsConstants.Error,
                    MessageKey = KeyMessage ?? LocalizationsConstants.FormErrorsMessage,
                    FeedbackType = FeedbackTypeEnum.Error,
                }
            }));
        }

        private EventCategoriesDetailViewModel FillData(EventCategoriesDetailViewModel source)
        {
            source.AvailabilitiesCategories = _availabilityCategoriesService.GetAllKeyValues().ToSelectList();
            return source;
        }
    }
}