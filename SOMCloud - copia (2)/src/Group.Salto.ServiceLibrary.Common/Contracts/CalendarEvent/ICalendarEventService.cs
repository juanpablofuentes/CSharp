using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Calendar;
using Group.Salto.ServiceLibrary.Common.Dtos.CalendarEvents;

namespace Group.Salto.ServiceLibrary.Common.Contracts.CalendarEvent
{
    public interface ICalendarEventService
    {
        ResultDto<CalendarDto> GetByCalendarId(int calendarId);
        ResultDto<CalendarEventDto> Create(CalendarEventDto calendarEvent);
        ResultDto<CalendarEventDto> Update(CalendarEventDto calendarEvent);
        ResultDto<bool> Delete(int id);
    }
}