using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Calendar;
using Group.Salto.ServiceLibrary.Common.Dtos.ProjectCalendar;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ProjectCalendarDtoExtensions
    {
        public static CalendarDto ToCalendarDto(this ProjectsCalendars source)
        {
            CalendarDto result = null;
            if (source != null)
            {
                result = new CalendarDto
                {
                    Id = source.Calendar.Id,
                    Name = source.Calendar.Name,
                    Description = source.Calendar.Description,
                    Color = source.Calendar.Color,
                    IsPrivate = source.Calendar.IsPrivate.HasValue ? source.Calendar.IsPrivate.Value : false,
                    Priority = source.CalendarPriority,
                    Events = source.Calendar.CalendarEvents.ToList().ToSchedulerDto()
                };
            }
            return result;
        }

        public static IList<CalendarDto> ToCalendarDto(this IList<ProjectsCalendars> source)
        {
            return source.MapList(x => x.ToCalendarDto());
        }

        public static ProjectsCalendars ToEntity(this ProjectCalendarDto source)
        {
            ProjectsCalendars result = null;
            if (source != null)
            {
                result = new ProjectsCalendars()
                {
                    ProjectId = source.ProjectId,
                    CalendarId = source.CalendarId,
                    CalendarPriority = source.Priority
                };
            }

            return result;
        }

        public static ProjectCalendarDto ToDto(this ProjectsCalendars source)
        {
            ProjectCalendarDto result = null;
            if (source != null)
            {
                result = new ProjectCalendarDto()
                {
                    ProjectId = source.ProjectId,
                    CalendarId = source.CalendarId,
                    Priority = source.CalendarPriority
                };
            }

            return result;
        }

        public static void UpdateProjectCalendar(this ProjectsCalendars target, ProjectsCalendars source)
        {
            if (source != null && target != null)
            {
                target.ProjectId = source.ProjectId;
                target.CalendarId = source.CalendarId;
                target.CalendarPriority = source.CalendarPriority;
            }
        }
    }
}