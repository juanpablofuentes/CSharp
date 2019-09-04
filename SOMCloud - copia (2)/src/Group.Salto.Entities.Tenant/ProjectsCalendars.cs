namespace Group.Salto.Entities.Tenant
{
    public class ProjectsCalendars
    {
        public int ProjectId { get; set; }
        public int CalendarId { get; set; }
        public int CalendarPriority { get; set; }

        public Calendars Calendar { get; set; }
        public Projects Project { get; set; }
    }
}
