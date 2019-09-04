using System;

namespace Group.Salto.Entities.Tenant
{
    public class RolesActionGroupsActionsTenant
    {
        public string RoleTenantId { get; set; }
        public Guid ActionGroupId { get; set; }
        public int ActionId { get; set; }

        public RolesTenant RolesTenant { get; set; }
    }
}