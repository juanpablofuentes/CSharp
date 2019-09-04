using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class LiteralPreconditionsRepository : BaseRepository<LiteralsPreconditions>, ILiteralPreconditionsRepository
    {
        public LiteralPreconditionsRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public LiteralsPreconditions GetLiteralPreconditions(int id)
        {
            return Filter(x => x.Id == id)
                    .Include(x => x.PreconditionsLiteralValues)
                    .FirstOrDefault();
        }

        public IQueryable<PreconditionsLiteralValues> GetLiteralValuesOfPrecondition(int preconditionId, Guid literalPreconditionType)
        {
            return Filter(x => x.PreconditionId == preconditionId && x.PreconditionsTypeId == literalPreconditionType)
                    .Include(x => x.PreconditionsLiteralValues).SelectMany(x=>x.PreconditionsLiteralValues);
        }

        public SaveResult<LiteralsPreconditions> UpdateLiteralPreconditions(LiteralsPreconditions entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Update(entity);
            var result = SaveChange(entity);
            return result;
        }

        public SaveResult<LiteralsPreconditions> CreateLiteralPreconditions(LiteralsPreconditions entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Create(entity);
            var result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }

        public SaveResult<LiteralsPreconditions> DeleteLiteralPreconditions(LiteralsPreconditions entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Delete(entity);
            var result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }

        public LiteralsPreconditions DeleteLiteralPreconditionsOnContext(LiteralsPreconditions entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Delete(entity);
            return entity;
        }
    }
}
