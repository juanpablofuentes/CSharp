namespace Group.Salto.ServiceLibrary.Common.Dtos.Calendar
{
    public class CalendarBaseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public bool IsPrivate { get; set; }
        public bool HasPeopleAssigned { get; set; }
    }
}