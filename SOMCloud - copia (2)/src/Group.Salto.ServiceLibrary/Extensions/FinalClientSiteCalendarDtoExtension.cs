using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Calendar;
using Group.Salto.ServiceLibrary.Common.Dtos.FinalClientSiteCalendar;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class FinalClientSiteCalendarDtoExtension
    {
        public static CalendarDto ToCalendarDto(this FinalClientSiteCalendar source)
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

        public static IList<CalendarDto> ToCalendarDto(this IList<FinalClientSiteCalendar> source)
        {
            return source.MapList(x => x.ToCalendarDto());
        }

        public static FinalClientSiteCalendar ToEntity(this FinalClientSiteCalendarDto source)
        {
            FinalClientSiteCalendar result = null;
            if (source != null)
            {
                result = new FinalClientSiteCalendar()
                {
                    FinalClientSiteId = source.FinalClientSiteId,
                    CalendarId = source.CalendarId,
                    CalendarPriority = source.Priority
                };
            }

            return result;
        }

        public static FinalClientSiteCalendarDto ToDto(this FinalClientSiteCalendar source)
        {
            FinalClientSiteCalendarDto result = null;
            if (source != null)
            {
                result = new FinalClientSiteCalendarDto()
                {
                    FinalClientSiteId = source.FinalClientSiteId,
                    CalendarId = source.CalendarId,
                    Priority = source.CalendarPriority
                };
            }

            return result;
        }

        public static void UpdateFinalClientSiteCalendar(this FinalClientSiteCalendar target, FinalClientSiteCalendar source)
        {
            if (source != null && target != null)
            {
                target.FinalClientSiteId = source.FinalClientSiteId;
                target.CalendarId = source.CalendarId;
                target.CalendarPriority = source.CalendarPriority;
            }
        }
    }
}
