using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.CalendarEvent;
using Group.Salto.Common.Constants.People;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Calendar;
using Group.Salto.ServiceLibrary.Common.Contracts.CalendarEvent;
using Group.Salto.ServiceLibrary.Common.Contracts.PeopleCalendar;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderCategoryCalendar;
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
    public class WorkOrderCategoryCalendarController : CalendarEventBaseController
    {
        private readonly IWorkOrderCategoryCalendarService _workOrderCategoryCalendarService;
        private readonly ICalendarService _calendarService;

        public WorkOrderCategoryCalendarController(ILoggingService loggingService,
                                        IWorkOrderCategoryCalendarService workOrderCategoryCalendarService,
                                        ICalendarEventService calendarEventService,
                                        ICalendarService calendarService) : base(loggingService, calendarEventService)
        {
            _calendarService = calendarService ?? throw new ArgumentNullException($"{nameof(ICalendarService)} is null");
            _workOrderCategoryCalendarService = workOrderCategoryCalendarService ?? throw new ArgumentNullException($"{nameof(IWorkOrderCategoryCalendarService)} is null");
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            _loggingService.LogInfo($"Get WorkOrderCategory by WorkOrderCategoryId {id}");
            IList<CalendarViewModel> calendars = _workOrderCategoryCalendarService.GetByWorkOrderCategoryIdNotDeleted(id).Data.ToViewModel();

            if (calendars == null)
            {
                return NotFound("Not data found");
            }
            return Ok(calendars);
        }

        [HttpGet("AvailableCalendars")]
        public IActionResult GetAvailableCalendars(int id)
        {
            _loggingService.LogInfo($"Get avariablecalendars WorkOrderCategory by WorkOrderCategoryId {id}");
            var calendars = _calendarService.GetKeyValuesAvailableWorkOrderCategoryCalendarsToAssign(id).ToKeyValuePair();

            if (calendars == null)
            {
                return NotFound("Not data found");
            }
            return Ok(calendars);
        }

        [HttpPost("AddCalendar")]
        public IActionResult PostAddNewCalendar([FromBody] AddCalendarViewModel data)
        {
            _loggingService.LogInfo($"Post WorkOrderCategory Create CreateNewCalendar");

            if (data == null)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.NotDataRequest) });
            }
            var result = _calendarService.AddPrivateCalendar(data.ToAddWorkOrderCategoryCalendarDto());

            if (result.Errors?.Errors != null && result.Errors?.Errors.Count > 0)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(PeopleConstants.PeopleCalendarCreateInCorrect) });
            }
            return Ok(new { result = true, text = LocalizedExtensions.GetUILocalizedText(PeopleConstants.PeopleCalendarCreateCorrect) });
        }

        [HttpPost]
        public IActionResult Post([FromBody] PrivateCalendarsViewModel data)
        {
            _loggingService.LogInfo($"Post Create WorkOrderCategory");

            if (data == null)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.NotDataRequest) });
            }
            var result = _workOrderCategoryCalendarService.Create(data.ToWorkOrderCategoryCalendarDto());

            if (result.Errors?.Errors != null && result.Errors?.Errors.Count > 0)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(PeopleConstants.PeopleCalendarAssignCreateInCorrect) });
            }
            return Ok(new { result = true, text = LocalizedExtensions.GetUILocalizedText(PeopleConstants.PeopleCalendarAssignCreateCorrect) });
        }

        [HttpPut]
        public IActionResult Put([FromBody] PrivateCalendarsViewModel data)
        {
            _loggingService.LogInfo($"Put Update WorkOrderCategory (For prioriry)");

            if (data == null)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.NotDataRequest) });
            }
            var result = _workOrderCategoryCalendarService.Update(data.ToWorkOrderCategoryCalendarDto());

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
            var result = _calendarService.Delete(data.ToWorkOrderCategoryCalendarDto());

            if (result.Errors?.Errors != null && result.Errors?.Errors.Count > 0)
            {
                return BadRequest(new { result = false, text = LocalizedExtensions.GetUILocalizedText(PeopleConstants.PeopleCalendarDeleteInCorrect) });
            }
            return Ok(new { result = true, text = LocalizedExtensions.GetUILocalizedText(PeopleConstants.PeopleCalendarDeleteCorrect) });
        }
    }
}