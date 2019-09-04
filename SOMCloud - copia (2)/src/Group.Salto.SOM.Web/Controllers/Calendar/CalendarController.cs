using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.Calendar;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Calendar;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Calendar;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Group.Salto.SOM.Web.Controllers.Calendar
{
    public class CalendarController : BaseController
    {
        private readonly ICalendarService _calendarService;

        public CalendarController(ILoggingService loggingService,
                                    IConfiguration configuration,
                                    ICalendarService calendarService,
                                    IHttpContextAccessor accessor) : base(loggingService, configuration, accessor)
        {
            _calendarService = calendarService ?? throw new ArgumentNullException($"{nameof(ICalendarService)} is null");
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.GlobalCalendar, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"Calendars.Get get all global calendars");
            var result = DoFilterAndPaging(new CalendarFilterViewModel());
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpPost]
        public IActionResult Filter(CalendarFilterViewModel filter)
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

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.GlobalCalendar, ActionEnum.Create)]
        public IActionResult Create()
        {
            LoggingService.LogInfo("Calendar.Create new calendar");
            return View();
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.GlobalCalendar, ActionEnum.GetById)]
        public IActionResult Edit(int id)
        {
            LoggingService.LogInfo($"Calendar.Get get calendar for id:{id}");
            var result = _calendarService.GetById(id);
            var response = result.ToResultViewModel();
            LogFeedbacks(response.Feedbacks?.Feedbacks);
            if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
            {
                return View(nameof(Edit), response);
            }
            SetFeedbackTempData(LocalizationsConstants.Error,
                LocalizationsConstants.ErrorLoadingDataMessage,
                FeedbackTypeEnum.Error);
            return Redirect(nameof(Index));
        }

        [HttpDelete]
        [CustomAuthorization(ActionGroupEnum.GlobalCalendar, ActionEnum.Delete)]
        public IActionResult Delete(int id)
        {
            LoggingService.LogInfo($"Calendar.Delete delete calendar for id:{id}");

            var deleteResult = _calendarService.Delete(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    CalendarsConstants.CalendarDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
                return Ok(deleteResult.Data);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                    CalendarsConstants.CalendarDeleteErrorMessage,
                    FeedbackTypeEnum.Error);
                return BadRequest(deleteResult.Data);
            }
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.GlobalCalendar, ActionEnum.Create)]
        public IActionResult Create(CalendarBaseViewModel model)
        {
            LoggingService.LogInfo("Calendar create");
            if (ModelState.IsValid)
            {
                var result = _calendarService.Create(model.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                        CalendarsConstants.CalendarCreateSuccess,
                        FeedbackTypeEnum.Success);
                    return RedirectToAction(nameof(Index));
                }
                var resultData = result.ToResultViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View(nameof(Create), resultData);
            }
            return View(nameof(Create), ProcessResult(model));
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.GlobalCalendar, ActionEnum.Update)]
        public IActionResult Edit(CalendarBaseViewModel model)
        {
            LoggingService.LogInfo($"Calendar update for id ={model.Id}");
            if (ModelState.IsValid)
            {
                var result = _calendarService.Update(model.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                        CalendarsConstants.CalendarUpdateSuccessMessage,
                        FeedbackTypeEnum.Success);
                    return RedirectToAction(nameof(Index));
                }
                var resultData = result.ToResultViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View(nameof(Edit), resultData);
            }
            return View(nameof(Edit), ProcessResult(model));
        }

        private ResultViewModel<CalendarsViewModel> DoFilterAndPaging(CalendarFilterViewModel filters)
        {
            var data = new ResultViewModel<CalendarsViewModel>();
            var filteredData = _calendarService.GetAllFiltered(filters.ToDto()).Data.ToViewModel();
            if (filters.Page != 0)
            {
                filters.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<CalendarBaseViewModel>()
                {
                    Page = filters.Page,
                    PageSize = filters.Size
                };
                data.Data = new CalendarsViewModel()
                {
                    Calendars = new MultiItemViewModel<CalendarBaseViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filters.PagesCount = pager.PagesCount;
                filters.Page = pager.Page;
                filters.TotalValues = filteredData.Count();
            }
            else
            {
                data.Data = new CalendarsViewModel()
                {
                    Calendars = new MultiItemViewModel<CalendarBaseViewModel, int>(filteredData)
                };
            }
            data.Data.Filters = filters;
            return data;
        }
    }
}