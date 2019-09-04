namespace Group.Salto.Entities.Tenant
{
    public class LocationCalendar
    {
        public int LocationId { get; set; }
        public int CalendarId { get; set; }
        public int CalendarPriority { get; set; }

        public Calendars Calendar { get; set; }
        public Locations Location { get; set; }
    }
}
