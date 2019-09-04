using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Entities.Tenant;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Microsoft.EntityFrameworkCore;
using Group.Salto.Common;
using System;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class CollectionsExtraFieldRepository : BaseRepository<CollectionsExtraField>, ICollectionsExtraFieldRepository
    {
        public CollectionsExtraFieldRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public Dictionary<int, string> GetAllKeyValues()
        {
            return All().Where(x=>!x.IsDeleted)
                .OrderBy(o => o.Name)
                .ToDictionary(t => t.Id, t => t.Name);
        }

        public IQueryable<CollectionsExtraField> GetAll()
        {
            return All().Where(x=>!x.IsDeleted);
        }

        public CollectionsExtraField GetById(int id)
        {
            return Find(x => x.Id == id);
        }

        public CollectionsExtraField GetByIdWithExtraFields(int id)
        {
            var query = Filter(x => x.Id == id)
                .Include(x => x.CollectionsExtraFieldExtraField)
                    .ThenInclude(x => x.ExtraField)
                        .ThenInclude(x => x.ErpSystemInstanceQuery);
            return query.FirstOrDefault();
        }

        public CollectionsExtraField GetByIdWithPredefinedServices(int id)
        {
            var query = Filter(x => x.Id == id)
                .Include(x => x.PredefinedServices)
                .Include(x => x.Projects);                    
            return query.FirstOrDefault();
        }

        public SaveResult<CollectionsExtraField> CreateCollectionsExtraField(CollectionsExtraField entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Create(entity);
            var result = SaveChange(entity);
            return result;
        }

        public SaveResult<CollectionsExtraField> UpdateCollectionsExtraField(CollectionsExtraField entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Update(entity);
            var result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }

        public SaveResult<CollectionsExtraField> DeleteCollectionsExtraField(CollectionsExtraField entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Delete(entity);
            var result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }
    }
}