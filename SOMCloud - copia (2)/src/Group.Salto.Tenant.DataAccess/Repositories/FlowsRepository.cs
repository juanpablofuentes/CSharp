using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using System;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class FlowsRepository : BaseRepository<Flows>, IFlowsRepository
    {
        public FlowsRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public Flows GetById(int id)
        {
            return Find(x => x.Id == id);
        }

        public SaveResult<Flows> CreateFlows(Flows entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Create(entity);
            var result = SaveChange(entity);
            return result;
        }

        public SaveResult<Flows> UpdateFlows(Flows entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Update(entity);
            var result = SaveChange(entity);
            return result;
        }

        public IQueryable<Flows> GetAll()
        {
            return All();
        }
    }
}