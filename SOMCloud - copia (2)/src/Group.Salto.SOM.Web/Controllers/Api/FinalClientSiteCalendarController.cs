using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.FinalClients;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Calendar;
using Group.Salto.ServiceLibrary.Common.Contracts.CalendarEvent;
using Group.Salto.ServiceLibrary.Common.Contracts.FinalClientSite;
using Group.Salto.SOM.Web.Extensions;
using Group.Salto.SOM.Web.Models.Calendar;
using Group.Salto.SOM.Web.Models.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Route("api/[controller]")]
    public class FinalClientSiteCalendarController : CalendarEventBaseController
    {
        private readonly ICalendarService _calendarService;
        private readonly IFinalClientSiteCalendarServices _finalClientSiteCalendarService;

        public FinalClientSiteCalendarController(ILoggingService loggingService,
            ICalendarEventService calendarEventService,
            ICalendarService calendarService,
            IFinalClientSiteCalendarServices finalClientSiteCalendarService) : base(loggingService, calendarEventService)
        {
            _calendarService = calendarService ?? throw new ArgumentNullException($"{nameof(ICalendarService)} is null");
            _finalClientSiteCalendarService = finalClientSiteCalendarService ?? throw new ArgumentNullException($"{nameof(IFinalClientSiteCalendarServices)} is null");
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            _loggingService.LogInfo($"Get FinalClient Calendars by FinalClientSite Id {id}");
            IList<CalendarViewModel> calendars = _finalClientSiteCalendarService.GetFinalClientSiteCalendarsNotDeletedByFinalClientSiteId(id).Data.ToViewModel();

            if (calendars == null)
            {
                return NotFound("Not data found");
            }
            return Ok(calendars);
        }

        [HttpGet("AvailableCalendars")]
        public IActionResult GetAvailableCalendars(int id)
        {
            _loggingService.LogInfo($"Get available FinalClientsCalendars by FinalClientsId {id}");
            var calendars = _calendarService.GetKeyValuesAvailablePeopleCalendarsToAssign(id).ToKeyValuePair();
            if (calendars == null)
            {
                return NotFound("Not data found");
            }
            return Ok(calendars);
        }

        [HttpPost("AddCalendar")]
        public IActionResult PostAddNewCalendar([FromBody] AddCalendarViewModel data)
        {
            _loggingService.LogInfo($"Post FinalClients Create CreateNewCalendar");
            if (data == null)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.NotDataRequest) });
            }
            var result = _calendarService.AddPrivateCalendar(data.ToAddFinalClientSiteCalendarDto());

            if (result.Errors?.Errors != null && result.Errors?.Errors.Count > 0)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(FinalClientsConstants.FinalClientSiteCalendarCreateInCorrect) });
            }
            return Ok(new { result = true, text = LocalizedExtensions.GetUILocalizedText(FinalClientsConstants.FinalClientSiteCalendarCreateCorrect) });
        }

        [HttpPost]
        public IActionResult Post([FromBody] PrivateCalendarsViewModel data)
        {
            _loggingService.LogInfo($"Post Create Calendar FinalClients");

            if (data == null)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.NotDataRequest) });
            }
            var result = _finalClientSiteCalendarService.Create(data.ToFinalClientSiteCalendarDto());

            if (result.Errors?.Errors != null && result.Errors?.Errors.Count > 0)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(FinalClientsConstants.FinalClientSiteCalendarAssignCreateInCorrect) });
            }
            return Ok(new { result = true, text = LocalizedExtensions.GetUILocalizedText(FinalClientsConstants.FinalClientSiteCalendarAssignCreateCorrect) });
        }

        [HttpPut]
        public IActionResult Put([FromBody] PrivateCalendarsViewModel data)
        {
            _loggingService.LogInfo($"Put Update FinalClient (For prioriry)");

            if (data == null)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.NotDataRequest) });
            }
            var result = _finalClientSiteCalendarService.Update(data.ToFinalClientSiteCalendarDto());

            if (result.Errors?.Errors != null && result.Errors?.Errors.Count > 0)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(FinalClientsConstants.FinalClientSiteCalendarUpdateInCorrect) });
            }
            return Ok(new { result = true, text = LocalizedExtensions.GetUILocalizedText(FinalClientsConstants.FinalClientSiteCalendarUpdateCorrect) });
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] PrivateCalendarsViewModel data)
        {
            _loggingService.LogInfo($"Delete Calendar");

            if (data == null)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.NotDataRequest) });
            }
            var result = _calendarService.Delete(data.ToFinalClientSiteCalendarDto());

            if (result.Errors?.Errors != null && result.Errors?.Errors.Count > 0)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(FinalClientsConstants.FinalClientSiteCalendarDeleteInCorrect) });
            }
            return Ok(new { result = true, text = LocalizedExtensions.GetUILocalizedText(FinalClientsConstants.FinalClientSiteCalendarDeleteCorrect) });
        }
    }
}