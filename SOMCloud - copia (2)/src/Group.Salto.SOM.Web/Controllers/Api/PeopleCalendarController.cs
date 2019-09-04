using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.CalendarEvent;
using Group.Salto.Common.Constants.People;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Calendar;
using Group.Salto.ServiceLibrary.Common.Contracts.CalendarEvent;
using Group.Salto.ServiceLibrary.Common.Contracts.PeopleCalendar;
using Group.Salto.SOM.Web.Extensions;
using Group.Salto.SOM.Web.Models.Calendar;
using Group.Salto.SOM.Web.Models.CalendarEvent;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.People;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class PeopleCalendarController : CalendarEventBaseController
    {
        private readonly IPeopleCalendarService _peopleCalendarService;
        private readonly ICalendarService _calendarService;

        public PeopleCalendarController(ILoggingService loggingService,
                                        IPeopleCalendarService peopleCalendarService,
                                        ICalendarEventService calendarEventService,
                                        ICalendarService calendarService) : base(loggingService, calendarEventService)
        {
            _calendarService = calendarService ?? throw new ArgumentNullException($"{nameof(ICalendarService)} is null");
            _peopleCalendarService = peopleCalendarService ?? throw new ArgumentNullException($"{nameof(IPeopleCalendarService)} is null");
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            _loggingService.LogInfo($"Get PeopleCalendar by personId {id}");
            IList<CalendarViewModel> calendars = _peopleCalendarService.GetByPeopleIdNotDeleted(id).Data.ToViewModel();
           
            if (calendars == null)
            {
                return NotFound("Not data found");
            }
            return Ok(calendars);
        }

        [HttpGet("AvailableCalendars")]
        public IActionResult GetAvailableCalendars(int id)
        {
            _loggingService.LogInfo($"Get PeopleCalendar by personId {id}");
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
            _loggingService.LogInfo($"Post Create CreateNewCalendar");

            if (data == null)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.NotDataRequest) });
            }
            var result = _calendarService.AddPrivateCalendar(data.ToAddPeopleCalendarDto());

            if (result.Errors?.Errors != null && result.Errors?.Errors.Count > 0)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(PeopleConstants.PeopleCalendarCreateInCorrect) });
            }
            return Ok(new { result = true, text = LocalizedExtensions.GetUILocalizedText(PeopleConstants.PeopleCalendarCreateCorrect) });
        }

        [HttpPost]
        public IActionResult Post([FromBody] PrivateCalendarsViewModel data)
        {
            _loggingService.LogInfo($"Post Create PeopleCalendar");

            if (data == null)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.NotDataRequest) });
            }
            var result = _peopleCalendarService.Create(data.ToPeopleCalendarDto());

            if (result.Errors?.Errors != null && result.Errors?.Errors.Count > 0)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(PeopleConstants.PeopleCalendarAssignCreateInCorrect) });
            }
            return Ok(new { result = true, text = LocalizedExtensions.GetUILocalizedText(PeopleConstants.PeopleCalendarAssignCreateCorrect) });
        }

        [HttpPut]
        public IActionResult Put([FromBody] PrivateCalendarsViewModel data)
        {
            _loggingService.LogInfo($"Put Update PeopleCalendar (For prioriry)");

            if (data == null)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.NotDataRequest) });
            }
            var result = _peopleCalendarService.Update(data.ToPeopleCalendarDto());

            if (result.Errors?.Errors != null && result.Errors?.Errors.Count > 0)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(PeopleConstants.PeopleCalendarUpdateInCorrect) });
            }
            return Ok(new { result = true, text = LocalizedExtensions.GetUILocalizedText(PeopleConstants.PeopleCalendarUpdateCorrect) });
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] PrivateCalendarsViewModel data)
        {
            _loggingService.LogInfo($"Delete Calendar");

            if (data == null)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.NotDataRequest) });
            }
            var result = _calendarService.Delete(data.ToPeopleCalendarDto());

            if (result.Errors?.Errors != null && result.Errors?.Errors.Count > 0)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(PeopleConstants.PeopleCalendarDeleteInCorrect) });
            }
            return Ok(new { result = true, text = LocalizedExtensions.GetUILocalizedText(PeopleConstants.PeopleCalendarDeleteCorrect) });
        }
    }
}