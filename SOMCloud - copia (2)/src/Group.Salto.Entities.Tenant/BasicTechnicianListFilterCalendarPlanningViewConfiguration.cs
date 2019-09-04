using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class BasicTechnicianListFilterCalendarPlanningViewConfiguration
    {
        public int TechnicianListFilterId { get; set; }
        public int CalendarPlanningViewConfigurationId { get; set; }

        public CalendarPlanningViewConfiguration CalendarPlanningViewConfiguration { get; set; }
        public BasicTechnicianListFilters TechnicianListFilter { get; set; }        
    }
}
