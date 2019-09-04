using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class SomFileRepository : BaseRepository<SomFiles>, ISomFileRepository
    {
        public SomFileRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public SaveResult<SomFiles> DeleteSomFile(SomFiles entity)
        {
            Delete(entity);
            SaveResult<SomFiles> result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }
    }
}