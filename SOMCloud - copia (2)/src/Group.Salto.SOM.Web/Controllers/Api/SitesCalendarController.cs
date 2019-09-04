using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.Sites;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Calendar;
using Group.Salto.ServiceLibrary.Common.Contracts.CalendarEvent;
using Group.Salto.ServiceLibrary.Common.Contracts.SitesCalendar;
using Group.Salto.SOM.Web.Extensions;
using Group.Salto.SOM.Web.Models.Calendar;
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
    public class SitesCalendarController : CalendarEventBaseController
    {
        private readonly ICalendarService _calendarService;
        private readonly ISitesCalendarServices _sitesCalendarServices;

        public SitesCalendarController(ILoggingService loggingService,
            ICalendarEventService calendarEventService,
            ICalendarService calendarService,
            ISitesCalendarServices siteCalendarService) : base(loggingService, calendarEventService)
        {
            _calendarService = calendarService ?? throw new ArgumentNullException($"{nameof(ICalendarService)} is null");
            _sitesCalendarServices = siteCalendarService ?? throw new ArgumentNullException($"{nameof(ISitesCalendarServices)} is null");
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            _loggingService.LogInfo($"Get SiteCalendars by SiteId {id}");
            IList<CalendarViewModel> calendars = _sitesCalendarServices.GetSitesCalendarsNotDeleted_BySiteId(id).Data.ToViewModel();

            if (calendars == null)
            {
                return NotFound("Not data found");
            }
            return Ok(calendars);
        }

        [HttpGet("AvailableCalendars")]
        public IActionResult GetAvailableCalendars(int id)
        {
            _loggingService.LogInfo($"Get available SiteCalendars by SiteId {id}");
            var calendars = _calendarService.GetKeyValuesAvailableSiteCalendarsToAssign(id).ToKeyValuePair();
            if (calendars == null)
            {
                return NotFound("Not data found");
            }
            return Ok(calendars);
        }

        [HttpPost("AddCalendar")]
        public IActionResult PostAddNewCalendar([FromBody] AddCalendarViewModel data)
        {
            _loggingService.LogInfo($"Post Site Create CreateNewCalendar");
            if (data == null)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.NotDataRequest) });
            }
            var result = _calendarService.AddPrivateCalendar(data.ToAddSiteCalendarDto());

            if (result.Errors?.Errors != null && result.Errors?.Errors.Count > 0)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(SitesConstants.SiteCalendarCreateInCorrect) });
            }
            return Ok(new { result = true, text = LocalizedExtensions.GetUILocalizedText(SitesConstants.SiteCalendarCreateCorrect) });
        }

        [HttpPost]
        public IActionResult Post([FromBody] PrivateCalendarsViewModel data)
        {
            _loggingService.LogInfo($"Post Create Calendar Site");

            if (data == null)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.NotDataRequest) });
            }
            var result = _sitesCalendarServices.Create(data.ToSiteCalendarDto());

            if (result.Errors?.Errors != null && result.Errors?.Errors.Count > 0)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(SitesConstants.SiteCalendarAssignCreateInCorrect) });
            }
            return Ok(new { result = true, text = LocalizedExtensions.GetUILocalizedText(SitesConstants.SiteCalendarAssignCreateCorrect) });
        }

        [HttpPut]
        public IActionResult Put([FromBody] PrivateCalendarsViewModel data)
        {
            _loggingService.LogInfo($"Put Update Site (For prioriry)");

            if (data == null)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.NotDataRequest) });
            }
            var result = _sitesCalendarServices.Update(data.ToSiteCalendarDto());

            if (result.Errors?.Errors != null && result.Errors?.Errors.Count > 0)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(SitesConstants.SiteCalendarUpdateInCorrect) });
            }
            return Ok(new { result = true, text = LocalizedExtensions.GetUILocalizedText(SitesConstants.SiteCalendarUpdateCorrect) });
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] PrivateCalendarsViewModel data)
        {
            _loggingService.LogInfo($"Delete Calendar");

            if (data == null)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.NotDataRequest) });
            }
            var result = _calendarService.Delete(data.ToSiteCalendarDto());

            if (result.Errors?.Errors != null && result.Errors?.Errors.Count > 0)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(SitesConstants.SiteCalendarDeleteInCorrect) });
            }
            return Ok(new { result = true, text = LocalizedExtensions.GetUILocalizedText(SitesConstants.SiteCalendarDeleteCorrect) });
        }
    }
}