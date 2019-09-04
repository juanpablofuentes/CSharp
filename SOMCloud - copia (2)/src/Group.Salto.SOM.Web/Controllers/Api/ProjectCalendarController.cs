using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.Project;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Calendar;
using Group.Salto.ServiceLibrary.Common.Contracts.CalendarEvent;
using Group.Salto.ServiceLibrary.Common.Contracts.ProjectCalendar;
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
    public class ProjectCalendarController : CalendarEventBaseController
    {
        private readonly ICalendarService _calendarService;
        private readonly IProjectCalendarService _projectCalendarService;

        public ProjectCalendarController(ILoggingService loggingService,
            ICalendarEventService calendarEventService,
            ICalendarService calendarService,
            IProjectCalendarService projectCalendarService) : base(loggingService, calendarEventService)
        {
            _calendarService = calendarService ?? throw new ArgumentNullException($"{nameof(ICalendarService)} is null");
            _projectCalendarService = projectCalendarService ?? throw new ArgumentNullException($"{nameof(IProjectCalendarService)} is null");
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            _loggingService.LogInfo($"Get Project Calendars by Project Id {id}");
            IList<CalendarViewModel> calendars = _projectCalendarService.GetProjectsCalendarsNotDeletedByProjectId(id).Data.ToViewModel();

            if (calendars == null)
            {
                return NotFound("Not data found");
            }
            return Ok(calendars);
        }

        [HttpGet("AvailableCalendars")]
        public IActionResult GetAvailableCalendars(int id)
        {
            _loggingService.LogInfo($"Get available calendars Project by ProjectId {id}");
            var calendars = _calendarService.GetKeyValuesAvailableProjectCalendarsToAssign(id).ToKeyValuePair();
            if (calendars == null)
            {
                return NotFound("Not data found");
            }
            return Ok(calendars);
        }

        [HttpPost("AddCalendar")]
        public IActionResult PostAddNewCalendar([FromBody] AddCalendarViewModel data)
        {
            _loggingService.LogInfo($"Post Project Create CreateNewCalendar");
            if (data == null)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.NotDataRequest) });
            }
            var result = _calendarService.AddPrivateCalendar(data.ToAddProjectCalendarDto());

            if (result.Errors?.Errors != null && result.Errors?.Errors.Count > 0)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(ProjectConstants.ProjectCalendarCreateInCorrect) });
            }
            return Ok(new { result = true, text = LocalizedExtensions.GetUILocalizedText(ProjectConstants.ProjectCalendarCreateCorrect) });
        }

        [HttpPost]
        public IActionResult Post([FromBody] PrivateCalendarsViewModel data)
        {
            _loggingService.LogInfo($"Post Create Calendar Project");

            if (data == null)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.NotDataRequest) });
            }
            var result = _projectCalendarService.Create(data.ToProjectCalendarDto());

            if (result.Errors?.Errors != null && result.Errors?.Errors.Count > 0)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(ProjectConstants.ProjectCalendarAssignCreateInCorrect) });
            }
            return Ok(new { result = true, text = LocalizedExtensions.GetUILocalizedText(ProjectConstants.ProjectCalendarAssignCreateCorrect) });
        }

        [HttpPut]
        public IActionResult Put([FromBody] PrivateCalendarsViewModel data)
        {
            _loggingService.LogInfo($"Put Update Project (For prioriry)");

            if (data == null)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.NotDataRequest) });
            }
            var result = _projectCalendarService.Update(data.ToProjectCalendarDto());

            if (result.Errors?.Errors != null && result.Errors?.Errors.Count > 0)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(ProjectConstants.ProjectCalendarUpdateInCorrect) });
            }
            return Ok(new { result = true, text = LocalizedExtensions.GetUILocalizedText(ProjectConstants.ProjectCalendarUpdateCorrect) });
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] PrivateCalendarsViewModel data)
        {
            _loggingService.LogInfo($"Delete Calendar");

            if (data == null)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.NotDataRequest) });
            }
            var result = _calendarService.Delete(data.ToProjectCalendarDto());

            if (result.Errors?.Errors != null && result.Errors?.Errors.Count > 0)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(ProjectConstants.ProjectCalendarDeleteInCorrect) });
            }
            return Ok(new { result = true, text = LocalizedExtensions.GetUILocalizedText(ProjectConstants.ProjectCalendarDeleteCorrect) });
        }
    }
}