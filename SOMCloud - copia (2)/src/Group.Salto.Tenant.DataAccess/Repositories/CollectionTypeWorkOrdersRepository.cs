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
    public class CollectionTypeWorkOrdersRepository : BaseRepository<CollectionsTypesWorkOrders>, ICollectionTypeWorkOrdersRepository
    {
        public CollectionTypeWorkOrdersRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<CollectionsTypesWorkOrders> GetAll()
        {
            return All();
        }

        public CollectionsTypesWorkOrders GetWorkOrderTypeById(int id)
        {
            var result = Filter(x => x.Id == id)
                .Include(x => x.WorkOrderTypes)
                    .ThenInclude(x => x.WorkOrderTypesFather)
                .FirstOrDefault();
            result.WorkOrderTypes = result.WorkOrderTypes.Where(x => !x.IsDeleted).ToList();
            return result;
        }

        public CollectionsTypesWorkOrders GetWorkOrderTypeAndProyectById(int id)
        {
            var result = Filter(x => x.Id == id)
                .Include(x => x.Projects)
                .Include(x => x.WorkOrderTypes)
                .FirstOrDefault();
            return result;
        }

        public SaveResult<CollectionsTypesWorkOrders> CreateCollectionsTypesWorkOrders(CollectionsTypesWorkOrders entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Create(entity);
            var result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }

        public SaveResult<CollectionsTypesWorkOrders> UpdateCollectionsTypesWorkOrders(CollectionsTypesWorkOrders entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Update(entity);
            var result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }

        public SaveResult<CollectionsTypesWorkOrders> DeleteCollectionsTypesWorkOrders(CollectionsTypesWorkOrders entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Delete(entity);
            var result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }
        
        public Dictionary<int, string> GetAllKeyValues()
        {
            return All()
                .OrderBy(o => o.Name)
                .ToDictionary(t => t.Id, t => t.Name);
        }
    }
}