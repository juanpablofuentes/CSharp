namespace Group.Salto.Entities.Tenant
{
    public class PlanningPanelViewConfigurationPeopleCollection
    {
        public int PlanningPanelViewConfigurationId { get; set; }
        public int PeopleCollectionId { get; set; }

        public PeopleCollections PeopleCollection { get; set; }
        public PlanningPanelViewConfiguration PlanningPanelViewConfiguration { get; set; }
    }
}
