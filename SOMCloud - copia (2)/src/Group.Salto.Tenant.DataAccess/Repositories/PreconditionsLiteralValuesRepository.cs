using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using System;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class PreconditionsLiteralValuesRepository : BaseRepository<PreconditionsLiteralValues>, IPreconditionsLiteralValuesRepository
    {
        public PreconditionsLiteralValuesRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public PreconditionsLiteralValues GetById(int id)
        {
            return Filter(t => t.Id == id).FirstOrDefault();
        }

        public SaveResult<PreconditionsLiteralValues> UpdatePreconditionsLiteralValues(PreconditionsLiteralValues entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Update(entity);
            var result = SaveChange(entity);
            return result;
        }

        public SaveResult<PreconditionsLiteralValues> CreatePreconditionsLiteralValues(PreconditionsLiteralValues entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Create(entity);
            var result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }

        public PreconditionsLiteralValues DeleteOnContextPreconditionsLiteralValues(PreconditionsLiteralValues entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Delete(entity);
            return entity;
        }

        public PreconditionsLiteralValues DeletePreconditionsLiteralValues(PreconditionsLiteralValues entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Delete(entity);
            SaveChange(entity);
            return entity;
        }
    }
}