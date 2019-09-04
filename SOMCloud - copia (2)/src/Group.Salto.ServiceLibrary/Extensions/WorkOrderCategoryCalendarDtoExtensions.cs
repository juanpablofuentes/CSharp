using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Calendar;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderCategoryCalendar;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class WorkOrderCategoryCalendarDtoExtensions
    {
        public static CalendarDto ToCalendarDto(this WorkOrderCategoryCalendar source)
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

        public static IList<CalendarDto> ToCalendarDto(this IList<WorkOrderCategoryCalendar> source)
        {
            return source.MapList(x => x.ToCalendarDto());
        }

        public static WorkOrderCategoryCalendarDto ToDto(this WorkOrderCategoryCalendar source)
        {
            WorkOrderCategoryCalendarDto result = null;
            if (source != null)
            {
                result = new WorkOrderCategoryCalendarDto()
                {
                    WorkOrderCategoryId = source.WorkOrderCategoryId,
                    CalendarId = source.CalendarId,
                    Priority = source.CalendarPriority
                };
            }

            return result;
        }

        public static WorkOrderCategoryCalendar ToEntity(this WorkOrderCategoryCalendarDto source)
        {
            WorkOrderCategoryCalendar result = null;
            if (source != null)
            {
                result = new WorkOrderCategoryCalendar()
                {
                    WorkOrderCategoryId = source.WorkOrderCategoryId,
                    CalendarId = source.CalendarId,
                    CalendarPriority = source.Priority
                };
            }

            return result;
        }

        public static void UpdateWorkOrderCategoryCalendar(this WorkOrderCategoryCalendar target, WorkOrderCategoryCalendar source)
        {
            if (source != null && target != null)
            {
                target.WorkOrderCategoryId = source.WorkOrderCategoryId;
                target.CalendarId = source.CalendarId;
                target.CalendarPriority = source.CalendarPriority;
            }
        }
    }
}