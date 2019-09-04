namespace Group.Salto.ServiceLibrary.Common.Dtos.CalendarEvents
{
    public class YearlyEventRepetition : EventRepetition
    {
        public int OnMonthNumber { get; set; }
        public int OnDayNumber { get; set; }
    }
}