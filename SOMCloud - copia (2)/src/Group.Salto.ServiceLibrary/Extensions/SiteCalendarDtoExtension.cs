using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Calendar;
using Group.Salto.ServiceLibrary.Common.Dtos.SiteCalendar;
using System.Collections.Generic;
using System.Linq;


namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class SiteCalendarDtoExtension
    {
        public static CalendarDto ToCalendarDto(this LocationCalendar source)
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

        public static IList<CalendarDto> ToCalendarDto(this IList<LocationCalendar> source)
        {
            return source.MapList(x => x.ToCalendarDto());
        }

        public static LocationCalendar ToEntity(this SiteCalendarDto source)
        {
            LocationCalendar result = null;
            if (source != null)
            {
                result = new LocationCalendar()
                {
                    LocationId = source.SiteId,
                    CalendarId = source.CalendarId,
                    CalendarPriority = source.Priority
                };
            }

            return result;
        }

        public static SiteCalendarDto ToDto(this LocationCalendar source)
        {
            SiteCalendarDto result = null;
            if (source != null)
            {
                result = new SiteCalendarDto()
                {
                    SiteId = source.LocationId,
                    CalendarId = source.CalendarId,
                    Priority = source.CalendarPriority
                };
            }

            return result;
        }

        public static void UpdateSiteCalendar(this LocationCalendar target, LocationCalendar source)
        {
            if (source != null && target != null)
            {
                target.LocationId = source.LocationId;
                target.CalendarId = source.CalendarId;
                target.CalendarPriority = source.CalendarPriority;
            }
        }
    }
}