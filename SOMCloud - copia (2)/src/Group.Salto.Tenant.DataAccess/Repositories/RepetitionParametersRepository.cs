using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class RepetitionParametersRepository : BaseRepository<RepetitionParameters>, IRepetitionParametersRepository
    {
        public RepetitionParametersRepository(ITenantUnitOfWork uow) : base(uow)
        {

        }

        public IQueryable<RepetitionParameters> GetAll()
        {
            return All();
        }

        public RepetitionParameters GetById(Guid id)
        {
            return Find(x => x.Id == id);
        }

        public RepetitionParameters GetFirst()
        {
            return All().FirstOrDefault();
        }

        public SaveResult<RepetitionParameters> UpdateRepetitionParameters(RepetitionParameters entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Update(entity);
            var result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }
    }
}