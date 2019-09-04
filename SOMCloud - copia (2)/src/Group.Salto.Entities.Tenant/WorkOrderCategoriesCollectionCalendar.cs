namespace Group.Salto.Entities.Tenant
{
    public class WorkOrderCategoriesCollectionCalendar
    {
        public int WorkOrderCategoriesCollectionId { get; set; }
        public int CalendarId { get; set; }
        public int CalendarPriority { get; set; }

        public Calendars Calendar { get; set; }
        public WorkOrderCategoriesCollections WorkOrderCategoriesCollection { get; set; }
    }
}
