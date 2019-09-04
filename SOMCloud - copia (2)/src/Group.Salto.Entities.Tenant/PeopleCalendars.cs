namespace Group.Salto.Entities.Tenant
{
    public class PeopleCalendars
    {
        public int PeopleId { get; set; }
        public int CalendarId { get; set; }
        public int CalendarPriority { get; set; }

        public Calendars Calendar { get; set; }
        public People People { get; set; }
    }
}
