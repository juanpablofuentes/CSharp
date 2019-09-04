using System;

namespace Group.Salto.ServiceLibrary.Common.Dtos.RolesActionGroupsActionsTenant
{
    public class RoleActionGroupActionTenantDto
    {
        public string RoleTenantId { get; set; }
        public Guid ActionGroupId { get; set; }
        public int ActionId { get; set; }
    }
}