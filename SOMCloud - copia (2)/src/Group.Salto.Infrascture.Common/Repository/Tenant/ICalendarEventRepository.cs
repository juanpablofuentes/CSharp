using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using System.Collections.Generic;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface ICalendarEventRepository : IRepository<CalendarEvents>
    {
        CalendarEvents GetById(int Id);
        IList<CalendarEvents> GetByCalendarId(int calendarId);
        IList<CalendarEvents> GetByCalendarIds(int[] calendarids);
        SaveResult<CalendarEvents> CreateCalendarEvent(CalendarEvents calendarEvent);
        SaveResult<CalendarEvents> UpdateCalendarEvent(CalendarEvents calendarEvent);
        SaveResult<CalendarEvents> DeleteCalendarEvent(CalendarEvents calendarEvent);
        CalendarEvents DeleteOnContext(CalendarEvents entity);
    }
}