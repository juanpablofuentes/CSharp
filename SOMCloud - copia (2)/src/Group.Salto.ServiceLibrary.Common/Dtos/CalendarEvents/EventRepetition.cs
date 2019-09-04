using System;

namespace Group.Salto.ServiceLibrary.Common.Dtos.CalendarEvents
{
    public class EventRepetition : EventRepetitionType
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public bool HasEnd { get; set; }
        public int NumberOfRepetitions { get; set; }
        public int RepetitionPeriod { get; set; }
        public int Duration { get; set; }
    }
}