using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.CalendarEvents;
using Itenso.TimePeriod;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders
{
    public class TimePeriodParameters
    {
        public TimePeriodParameters() { }

        public TimePeriodParameters(CalendarEventDto calendarEvent, List<CalendarEventDto> calendarEvents, DateTime start, DateTime end)
        {
            CalendarEvent = calendarEvent;
            CalendarEvents = calendarEvents;
            Start = start;
            End = end;
        }

        public List<OrderedCalendarsDto> OrderedCalendars { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Minutes { get; set; }
        public CancellationToken Token { get; set; }
        public ITimePeriodCollection Collection1 { get; set; }
        public ITimePeriodCollection Collection2 { get; set; }
        public TimePeriodCombiner<TimeRange> Combiner { get; set; } = null;
        public TimePeriodSubtractor<TimeRange> Substractor { get; set; } = null;
        public List<Calendars> Calendars { get; set; }
        public Calendars Calendar { get; set; }
        public List<CalendarEventDto> CalendarEvents { get; set; }
        public CalendarEventDto CalendarEvent { get; set; }
    }
}