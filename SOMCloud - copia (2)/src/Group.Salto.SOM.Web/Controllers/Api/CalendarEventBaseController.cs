using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.CalendarEvent;
using Group.Salto.ServiceLibrary.Common.Contracts.PeopleCalendar;
using Group.Salto.SOM.Web.Extensions;
using Group.Salto.SOM.Web.Models.CalendarEvent;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.CalendarEvent;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarEventBaseController : ControllerBase
    {
        protected readonly ILoggingService _loggingService;
        protected readonly ICalendarEventService _calendarEventService;

        public CalendarEventBaseController(ILoggingService loggingService,
                                           ICalendarEventService calendarEventService)
        {
            _loggingService = loggingService ?? throw new ArgumentNullException($"{nameof(ILoggingService)} is null");
            _calendarEventService = calendarEventService ?? throw new ArgumentNullException($"{nameof(ICalendarEventService)} is null");
        }

        [HttpPost("SaveEvent")]
        public IActionResult CreateCalendarEvent([FromBody] CalendarEventRequestViewModel data)
        {
            _loggingService.LogInfo($"Post CalendarEvents");

            if (data == null)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.NotDataRequest) });
            }
            var result = _calendarEventService.Create(data.ToDto());
            if (result.Errors?.Errors != null && result.Errors?.Errors.Count > 0)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(CalendarEventConstants.CalendarEventCreationInCorrect) });
            }
            return Ok(new { result = true, text = LocalizedExtensions.GetUILocalizedText(CalendarEventConstants.CalendarEventCreationCorrect) });
        }

        [HttpPut("SaveEvent")]
        public IActionResult UpdateCalendarEvent([FromBody] CalendarEventRequestViewModel data)
        {
            _loggingService.LogInfo($"Put CalendarEvents");

            if (data == null)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.NotDataRequest) });
            }
            var result = _calendarEventService.Update(data.ToDto());
            if (result.Errors?.Errors != null && result.Errors?.Errors.Count > 0)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(CalendarEventConstants.CalendarEventUpdateInCorrect) });
            }
            return Ok(new { result = true, text = LocalizedExtensions.GetUILocalizedText(CalendarEventConstants.CalendarEventUpdateCorrect) });
        }

        [HttpDelete("SaveEvent")]
        public IActionResult DeleteCalendarEvent([FromBody] int id)
        {
            _loggingService.LogInfo($"Delete CalendarEvents: {id}");
            if (id == 0)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.NotDataRequest) });
            }
            var result = _calendarEventService.Delete(id);
            if (result.Errors?.Errors != null && result.Errors?.Errors.Count > 0)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(CalendarEventConstants.CalendarEventDeleteInCorrect) });
            }
            return Ok(new { result = true, text = LocalizedExtensions.GetUILocalizedText(CalendarEventConstants.CalendarEventDeleteCorrect) });
        }
    }
}