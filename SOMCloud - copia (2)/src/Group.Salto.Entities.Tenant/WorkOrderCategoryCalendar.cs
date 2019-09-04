namespace Group.Salto.Entities.Tenant
{
    public class WorkOrderCategoryCalendar
    {
        public int WorkOrderCategoryId { get; set; }
        public int CalendarId { get; set; }
        public int CalendarPriority { get; set; }

        public Calendars Calendar { get; set; }
        public WorkOrderCategories WorkOrderCategory { get; set; }
    }
}
