using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class BasicTechnicianListFilters : BaseEntity
    {
        public decimal? WorkLoad { get; set; }
        public decimal? MaxDistance { get; set; }

        public TechnicianListFilters TechnicianListFilters { get; set; }
        public ICollection<BasicTechnicianListFilterSkills> BasicTechnicianListFilterSkills { get; set; }
        public ICollection<BasicTechnicianListFilterCalendarPlanningViewConfiguration> BasicTechnicianListFilterCalendarPlanningViewConfiguration { get; set; }
    }
}
