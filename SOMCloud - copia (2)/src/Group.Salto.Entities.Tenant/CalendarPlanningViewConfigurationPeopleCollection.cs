namespace Group.Salto.Entities.Tenant
{
    public class CalendarPlanningViewConfigurationPeopleCollection
    {
        public int ViewId { get; set; }
        public int PeopleCollectionId { get; set; }

        public PeopleCollections PeopleCollection { get; set; }
        public CalendarPlanningViewConfiguration View { get; set; }
    }
}
