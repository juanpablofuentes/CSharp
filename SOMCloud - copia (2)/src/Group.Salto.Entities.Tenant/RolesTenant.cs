using System;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class RolesTenant
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime UpdateDate { get; set; }

        public ICollection<RolesActionGroupsActionsTenant> RolesActionGroupsActionsTenant { get; set; }
        public ICollection<UserConfigurationRolesTenant> UserConfigurationRolesTenant { get; set; }
    }
}