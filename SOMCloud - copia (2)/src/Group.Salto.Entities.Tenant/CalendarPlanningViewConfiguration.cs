using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class CalendarPlanningViewConfiguration : BaseEntity
    {
        public string Name { get; set; }
        public bool? IsDefault { get; set; }
        public int? CalendarPlanningViewConfigurationId { get; set; }
        public int? UserConfigurationId { get; set; }
        public UserConfiguration UserConfiguration { get; set; }
        public UsersMainWoviewConfigurations CalendarPlanningViewConfigurationNavigation { get; set; }
        public ICollection<CalendarPlanningViewConfigurationPeople> CalendarPlanningViewConfigurationPeople { get; set; }
        public ICollection<CalendarPlanningViewConfigurationPeopleCollection> CalendarPlanningViewConfigurationPeopleCollection { get; set; }
        public ICollection<BasicTechnicianListFilterCalendarPlanningViewConfiguration> BasicTechnicianListFilterCalendarPlanningViewConfiguration { get; set; }
    }
}
