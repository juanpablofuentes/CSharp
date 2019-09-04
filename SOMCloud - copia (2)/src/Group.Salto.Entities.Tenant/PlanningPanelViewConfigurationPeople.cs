namespace Group.Salto.Entities.Tenant
{
    public class PlanningPanelViewConfigurationPeople
    {
        public int PlanningPanelViewConfigurationId { get; set; }
        public int PeopleId { get; set; }

        public People People { get; set; }
        public PlanningPanelViewConfiguration PlanningPanelViewConfiguration { get; set; }
    }
}
