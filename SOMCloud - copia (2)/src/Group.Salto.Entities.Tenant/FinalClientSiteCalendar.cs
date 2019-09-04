namespace Group.Salto.Entities.Tenant
{
    public class FinalClientSiteCalendar
    {
        public int FinalClientSiteId { get; set; }
        public int CalendarId { get; set; }
        public int CalendarPriority { get; set; }

        public Calendars Calendar { get; set; }
        public FinalClients FinalClientSite { get; set; }
    }
}
