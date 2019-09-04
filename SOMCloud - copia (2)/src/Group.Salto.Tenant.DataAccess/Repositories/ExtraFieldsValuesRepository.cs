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
    public class ExtraFieldsValuesRepository : BaseRepository<ExtraFieldsValues>, IExtraFieldsValuesRepository
    {
        public ExtraFieldsValuesRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public ExtraFieldsValues GetById (int id)
        {
            return Filter(ef => ef.Id == id).FirstOrDefault();
        }

        public IQueryable<ExtraFieldsValues> GetAllExtrafields (int id)
        {
            return Filter(s => s.ServiceId == id)
                .Include(ef => ef.ExtraField);
        }

        public SaveResult<ExtraFieldsValues> UpdateExtraFieldValues(List<ExtraFieldsValues> entities)
        {
            foreach (var entity in entities)
            {
                entity.UpdateDate = DateTime.UtcNow;
                Update(entity);
            }
            var result = SaveChanges();
            return result;
        }
    }
}