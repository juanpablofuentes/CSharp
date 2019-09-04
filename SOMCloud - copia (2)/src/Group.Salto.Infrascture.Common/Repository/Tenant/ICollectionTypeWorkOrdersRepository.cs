using Group.Salto.Entities.Tenant;
using System.Linq;
using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface ICollectionTypeWorkOrdersRepository : IRepository<CollectionsTypesWorkOrders>
    {
        IQueryable<CollectionsTypesWorkOrders> GetAll();
        CollectionsTypesWorkOrders GetWorkOrderTypeById(int id);
        CollectionsTypesWorkOrders GetWorkOrderTypeAndProyectById(int id);
        SaveResult<CollectionsTypesWorkOrders> CreateCollectionsTypesWorkOrders(CollectionsTypesWorkOrders entity);
        SaveResult<CollectionsTypesWorkOrders> UpdateCollectionsTypesWorkOrders(CollectionsTypesWorkOrders entity);
        SaveResult<CollectionsTypesWorkOrders> DeleteCollectionsTypesWorkOrders(CollectionsTypesWorkOrders entity);
        Dictionary<int, string> GetAllKeyValues();
    }
}