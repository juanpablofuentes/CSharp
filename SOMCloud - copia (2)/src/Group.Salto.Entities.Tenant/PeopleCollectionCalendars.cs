namespace Group.Salto.Entities.Tenant
{
    public class PeopleCollectionCalendars
    {
        public int PeopleCollectionId { get; set; }
        public int CalendarId { get; set; }
        public int CalendarPriority { get; set; }

        public Calendars Calendar { get; set; }
        public PeopleCollections PeopleCollection { get; set; }
    }
}
