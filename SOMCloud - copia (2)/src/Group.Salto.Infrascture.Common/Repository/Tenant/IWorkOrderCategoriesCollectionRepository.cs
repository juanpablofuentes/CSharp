using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IWorkOrderCategoriesCollectionRepository : IRepository<WorkOrderCategoriesCollections>
    {
        IQueryable<WorkOrderCategoriesCollections> GetAll();
        WorkOrderCategoriesCollections GetById(int id);
        WorkOrderCategoriesCollections GetWithCategoriesById(int id);
        WorkOrderCategoriesCollections GetWithProjectById(int id);
        SaveResult<WorkOrderCategoriesCollections> CreateWorkOrderCategoriesCollections(WorkOrderCategoriesCollections entity);
        SaveResult<WorkOrderCategoriesCollections> UpdateWorkOrderCategoriesCollections(WorkOrderCategoriesCollections entity);
        Dictionary<int, string> GetAllKeyValues();
        SaveResult<WorkOrderCategoriesCollections> DeletesWorkOrdersCategoryCollection(WorkOrderCategoriesCollections entity);
    }
}