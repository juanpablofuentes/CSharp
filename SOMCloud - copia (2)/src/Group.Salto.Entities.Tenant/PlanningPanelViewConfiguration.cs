using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class PlanningPanelViewConfiguration : BaseEntity
    {
        public int? PeopleOwnerId { get; set; }
        public string Name { get; set; }
        public bool? IsDefault { get; set; }
        public int? UsersMainWoViewConfigurationId { get; set; }
        public int? StartViewTime { get; set; }
        public int? EndViewTime { get; set; }

        public People PeopleOwner { get; set; }
        public UsersMainWoviewConfigurations UsersMainWoViewConfiguration { get; set; }
        public ICollection<PlanningPanelViewConfigurationPeople> PlanningPanelViewConfigurationPeople { get; set; }
        public ICollection<PlanningPanelViewConfigurationPeopleCollection> PlanningPanelViewConfigurationPeopleCollection { get; set; }
    }
}
