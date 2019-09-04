using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Calendar;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class CalendarDtoExtensions
    {
        public static CalendarDto ToCalendarPeopleDto(this Calendars source, int peopleId)
        {
            CalendarDto result = null;
            if (source != null)
            {
                result = new CalendarDto
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    Color = source.Color,
                    IsPrivate = source.IsPrivate.HasValue ? source.IsPrivate.Value : false,
                    Priority = 1,
                    Events = source.CalendarEvents.ToList().ToSchedulerDto()
                };

                if (source.PeopleCalendars != null)
                { 
                    result.Priority = source.PeopleCalendars.FirstOrDefault(x => x.CalendarId == source.Id && x.PeopleId == peopleId).CalendarPriority;
                }
            }
            return result;
        }

        public static IList<CalendarDto> ToCalendarPeopleDto(this IList<Calendars> source, int peopleId)
        {
            return source.MapList(x => x.ToCalendarPeopleDto(peopleId));
        }
    }
}