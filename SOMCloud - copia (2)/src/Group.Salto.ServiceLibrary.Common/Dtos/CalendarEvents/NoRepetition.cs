using System;

namespace Group.Salto.ServiceLibrary.Common.Dtos.CalendarEvents
{
    public class NoRepetition : EventRepetitionType
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}