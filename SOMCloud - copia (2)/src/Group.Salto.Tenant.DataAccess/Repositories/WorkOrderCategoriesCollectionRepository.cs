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
    public class WorkOrderCategoriesCollectionRepository : BaseRepository<WorkOrderCategoriesCollections>, IWorkOrderCategoriesCollectionRepository
    {
        public WorkOrderCategoriesCollectionRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<WorkOrderCategoriesCollections> GetAll()
        {
            return All();
        }

        public WorkOrderCategoriesCollections GetById(int id)
        {
            var result = Find(x => x.Id == id);
            return result;
        }

        public WorkOrderCategoriesCollections GetWithProjectById(int id)
        {
            var result = Filter(x => x.Id == id)
                .Include(x => x.Projects).FirstOrDefault();
            return result;
        }

        public WorkOrderCategoriesCollections GetWithCategoriesById(int id)
        {
            return Filter(x => x.Id == id)
                 .Include(x => x.WorkOrderCategories)
                 .FirstOrDefault();
        }

        public Dictionary<int, string> GetAllKeyValues()
        {
            return All()
                .OrderBy(o => o.Name)
                .ToDictionary(t => t.Id, t => t.Name);
        }

        public SaveResult<WorkOrderCategoriesCollections> CreateWorkOrderCategoriesCollections(WorkOrderCategoriesCollections entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Create(entity);
            var result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }

        public SaveResult<WorkOrderCategoriesCollections> UpdateWorkOrderCategoriesCollections(WorkOrderCategoriesCollections entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Update(entity);
            var result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }

        public SaveResult<WorkOrderCategoriesCollections> DeletesWorkOrdersCategoryCollection(WorkOrderCategoriesCollections entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Delete(entity);
            SaveResult<WorkOrderCategoriesCollections> result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }
    }
}