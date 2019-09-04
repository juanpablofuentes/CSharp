using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Microsoft.EntityFrameworkCore;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class CollectionsClosureCodesRepository : BaseRepository<CollectionsClosureCodes>, ICollectionsClosureCodesRepository
    {
        public CollectionsClosureCodesRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<CollectionsClosureCodes> GetAll()
        {
            return Filter(x => !x.IsDeleted);
        }

        public CollectionsClosureCodes GetById(int id)
        {
            var result = Filter(x => x.Id == id)
                .Include(x => x.ClosingCodes)
                    .ThenInclude(x => x.ClosingCodesFather)
                .SingleOrDefault();
            return result;
        }

        public SaveResult<CollectionsClosureCodes> CreateClosureCode(CollectionsClosureCodes entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Create(entity);
            var result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }

        public SaveResult<CollectionsClosureCodes> UpdateClosureCode(CollectionsClosureCodes entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Update(entity);
            var result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }

        public SaveResult<CollectionsClosureCodes> DeleteClosureCode(CollectionsClosureCodes entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Delete(entity);
            var result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }

        public Dictionary<int, string> GetAllKeyValues()
        {
            return Filter(x => !x.IsDeleted)
                .OrderBy(o => o.Name)
                .ToDictionary(t => t.Id, t => t.Name);
        }
    }
}