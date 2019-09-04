namespace Group.Salto.ServiceLibrary.Common.Dtos.CalendarEvents
{
    public class WeeklyEventRepetition : EventRepetition
    {
        public bool[] OnWeekDays { get; set; }
    }
}