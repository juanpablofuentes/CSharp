using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using System;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class RolesTenantRepository : BaseRepository<RolesTenant>, IRolesTenantRepository
    {
        public RolesTenantRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<RolesTenant> GetAll()
        {
            return All();
        }

        public RolesTenant GetById(string id)
        {
            return Find(rt => rt.Id == id);
        }

        public int GetMaxId()
        {
            return All().Select(r => Convert.ToInt32(r.Id)).Max();
        }

        public SaveResult<RolesTenant> CreateRolTenant(RolesTenant entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Create(entity);
            var result = SaveChange(entity);
            return result;
        }

        public SaveResult<RolesTenant> UpdateRolTenant(RolesTenant entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Update(entity);
            var result = SaveChange(entity);
            return result;
        }
    }
}