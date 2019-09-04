using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Calendar;
using Group.Salto.ServiceLibrary.Common.Dtos.PeopleCalendar;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class PeopleCalendarDtoExtensions
    {
        public static CalendarDto ToCalendarDto(this PeopleCalendars source)
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

        public static IList<CalendarDto> ToCalendarDto(this IList<PeopleCalendars> source)
        {
            return source.MapList(x => x.ToCalendarDto());
        }

        public static PeopleCalendarDto ToDto(this PeopleCalendars source)
        {
            PeopleCalendarDto result = null;
            if (source != null)
            {
                result = new PeopleCalendarDto()
                {
                    PeopleId = source.PeopleId,
                    CalendarId = source.CalendarId,
                    Priority = source.CalendarPriority
                };
            }

            return result;
        }

        public static PeopleCalendars ToEntity(this PeopleCalendarDto source)
        {
            PeopleCalendars result = null;
            if (source != null)
            {
                result = new PeopleCalendars()
                {
                  
                    PeopleId = source.PeopleId,
                    CalendarId = source.CalendarId,
                    CalendarPriority = source.Priority
                };
            }

            return result;
        }

        public static void UpdatePeopleCalendar(this PeopleCalendars target, PeopleCalendars source)
        {
            if (source != null && target != null)
            {
                target.PeopleId = source.PeopleId;
                target.CalendarId = source.CalendarId;
                target.CalendarPriority = source.CalendarPriority;
            }
        }
    }
}