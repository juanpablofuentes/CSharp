using Group.Salto.Common;
using System;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class CalendarEvents : BaseEntity
    {
        public int? CalendarId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Category { get; set; }
        public int? RepetitionType { get; set; }
        public TimeSpan? StartTime { get; set; }
        public int? Duration { get; set; }
        public bool? HasEnd { get; set; }
        public int? NumberOfRepetitions { get; set; }
        public int? RepetitionPeriod { get; set; }
        public bool? RepeatOnMonday { get; set; }
        public bool? RepeatOnTuesday { get; set; }
        public bool? RepeatOnWednesday { get; set; }
        public bool? RepeatOnThursday { get; set; }
        public bool? RepeatOnFriday { get; set; }
        public bool? RepeatOnSaturday { get; set; }
        public bool? RepeatOnSunday { get; set; }
        public int? RepeatOnDayNumber { get; set; }
        public int? RepeatOnMonthNumber { get; set; }
        public string Color { get; set; }
        public int? ParentEventId { get; set; }
        public bool? DeletedOccurrence { get; set; }
        public long? ReplacedEventOccurrenceTs { get; set; }
        public double? CostHour { get; set; }
        public DateTime ModificationDate { get; set; }

        public Calendars Calendar { get; set; }
        public CalendarEvents ParentEvent { get; set; }
        public ICollection<CalendarEvents> InverseParentEvent { get; set; }
    }
}
