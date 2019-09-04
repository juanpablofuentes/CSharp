using Group.Salto.Entities.Tenant;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IRolesActionGroupsActionsTenantRepository
    {
        IQueryable<RolesActionGroupsActionsTenant> GetAllRolesActionGroupsActionsTenant();
        IQueryable<RolesActionGroupsActionsTenant> GetRolesActionsByRolId(string rolId);
        bool DeleteListRolesActionGroupsActionsTenant(IEnumerable<RolesActionGroupsActionsTenant> entities);
    }
}