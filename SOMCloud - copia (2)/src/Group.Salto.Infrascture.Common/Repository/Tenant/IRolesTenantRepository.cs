using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IRolesTenantRepository
    {
        IQueryable<RolesTenant> GetAll();
        RolesTenant GetById(string id);
        int GetMaxId();
        SaveResult<RolesTenant> CreateRolTenant(RolesTenant entity);
        SaveResult<RolesTenant> UpdateRolTenant(RolesTenant entity);
    }
}