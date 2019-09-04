namespace Group.Salto.Entities.Tenant
{
    public class CalendarPlanningViewConfigurationPeople
    {
        public int ViewId { get; set; }
        public int PeopleId { get; set; }

        public People People { get; set; }
        public CalendarPlanningViewConfiguration View { get; set; }
    }
}
