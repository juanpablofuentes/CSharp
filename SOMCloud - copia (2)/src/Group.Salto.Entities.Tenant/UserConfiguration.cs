using System;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class UserConfiguration
    {
        public int Id { get; set; }
        public Guid? GuidId { get; set; }
        public ICollection<Audits> AuditsUser { get; set; }
        public ICollection<Audits> AuditsUserSupplanter { get; set; }
        public ICollection<CalendarPlanningViewConfiguration> CalendarPlanningViewConfiguration { get; set; }
        public ICollection<People> People { get; set; }
        public ICollection<ServicesViewConfigurations> ServicesViewConfigurations { get; set; }
        public ICollection<Sessions> Sessions { get; set; }
        public ICollection<UsersMainWoviewConfigurations> UsersMainWoviewConfigurations { get; set; }
        public ICollection<UserConfigurationRolesTenant> UserConfigurationRolesTenant { get; set; }
    }
}