using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.CalendarEvent;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.CalendarEvent;
using Group.Salto.SOM.Web.Extensions;
using Group.Salto.SOM.Web.Models.Calendar;
using Group.Salto.SOM.Web.Models.CalendarEvent;
using Group.Salto.SOM.Web.Models.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class CalendarEventsController : CalendarEventBaseController
    {
        public CalendarEventsController(ILoggingService loggingService,
                                        ICalendarEventService calendarEventService) : base(loggingService, calendarEventService)
        {
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            _loggingService.LogInfo($"Get CalendarEvents by id {id}");
            CalendarViewModel calendar = _calendarEventService.GetByCalendarId(id).Data.ToViewModel();
            if (calendar == null)
            {
                return NotFound("Not data found");
            }
            return Ok(new List<CalendarViewModel>() { calendar });
        }
    }
}