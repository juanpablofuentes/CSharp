using Group.Salto.ServiceLibrary.Common.Dtos.CalendarEvents;
using Itenso.TimePeriod;
using System;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Calendar
{
    public class CalendarEventTimeRange : TimeRange
    {
        public CalendarEventDto Event { get; set; }
        public CalendarEventTimeRange(DateTime Start, DateTime End, CalendarEventDto Event) : base(Start, End, false)
        {
            this.Event = Event;
        }
    }
}