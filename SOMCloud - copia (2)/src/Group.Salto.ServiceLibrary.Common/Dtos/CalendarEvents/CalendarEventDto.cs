namespace Group.Salto.ServiceLibrary.Common.Dtos.CalendarEvents
{
    public class CalendarEventDto
    {
        public int Id { get; set; }
        public int CalendarId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string Color { get; set; }
        public double? CostHour { get; set; }
        public EventRepetitionType Repetition { get; set; }
        public long ReplacedEventOccurrenceTS { get; set; }
        public bool DeletedOccurrence { get; set; }
        public int ParentEventId { get; set; }
    }
}