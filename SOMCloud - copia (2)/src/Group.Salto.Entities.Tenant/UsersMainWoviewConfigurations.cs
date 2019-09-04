using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class UsersMainWoviewConfigurations : BaseEntity
    {
        public int UserId { get; set; }
        public bool? IsDefault { get; set; }
        public string Name { get; set; }
        public int UserConfigurationId { get; set; }
        public UserConfiguration UserConfiguration { get; set; }

        public ICollection<CalendarPlanningViewConfiguration> CalendarPlanningViewConfiguration { get; set; }
        public ICollection<MainWoViewConfigurationsColumns> MainWoViewConfigurationsColumns { get; set; }
        public ICollection<MainWoViewConfigurationsPeople> MainWoViewConfigurationsPeople { get; set; }
        public ICollection<MainWoviewConfigurationsGroups> MainWoviewConfigurationsGroups { get; set; }
        public ICollection<PlanningPanelViewConfiguration> PlanningPanelViewConfiguration { get; set; }
    }
}
