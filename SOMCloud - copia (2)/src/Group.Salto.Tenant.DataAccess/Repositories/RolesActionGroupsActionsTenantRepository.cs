using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class RolesActionGroupsActionsTenantRepository : BaseRepository<RolesActionGroupsActionsTenant>, IRolesActionGroupsActionsTenantRepository
    {
        public RolesActionGroupsActionsTenantRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<RolesActionGroupsActionsTenant> GetAllRolesActionGroupsActionsTenant()
        {
            return All();
        }

        public IQueryable<RolesActionGroupsActionsTenant> GetRolesActionsByRolId(string rolId)
        {
            return Filter(x => x.RoleTenantId == rolId);
        }

        public bool DeleteListRolesActionGroupsActionsTenant(IEnumerable<RolesActionGroupsActionsTenant> entities)
        {
            return DeleteRange(entities);
        }
    }
}