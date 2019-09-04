using System;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Calendar;
using Group.Salto.ServiceLibrary.Common.Contracts.CalendarEvent;
using Group.Salto.ServiceLibrary.Common.Contracts.EventCategories;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.Scheduler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Controllers.Scheduler
{
    [Authorize]
    public class SchedulerController : BaseController
    {
        private readonly ICalendarService _calendarService;
        private readonly IEventCategoriesService _eventCategoriesService;

        public SchedulerController(ILoggingService loggingService, 
                                    IConfiguration configuration, 
                                    IHttpContextAccessor accessor,
                                    ICalendarService calendarService,
                                    IEventCategoriesService eventCategoriesService) : base(loggingService, configuration, accessor)
        {
            _calendarService = calendarService ?? throw new ArgumentNullException($"{nameof(ICalendarService)} is null");
            _eventCategoriesService = eventCategoriesService ?? throw new ArgumentNullException($"{nameof(IEventCategoriesService)} is null");
        }

        public IActionResult Index(int Id)
        {
            LoggingService.LogInfo($"SchedulerController.Get get calendarEvent for calendarId:{Id}");
            ResultViewModel<SchedulerViewModel> scheduler = new ResultViewModel<SchedulerViewModel>();
            scheduler.Data = new SchedulerViewModel() { Id = Id };
            scheduler.Data.CalendarEventCategory = _eventCategoriesService.GetAllKeyValuesNotDeleted();
            return View(scheduler);
        }
    }
}