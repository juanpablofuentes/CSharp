using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common;
using Group.Salto.Infrastructure.Common.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public interface IWorkOrderCategoriesRepository : IRepository<WorkOrderCategories>
    {
        WorkOrderCategories GetById(int id);
        SaveResult<WorkOrderCategories> CreateWorkOrderCategories(WorkOrderCategories workOrderCategory);
        SaveResult<WorkOrderCategories> UpdateWorkOrderCategories(WorkOrderCategories workOrderCategory);
        SaveResult<WorkOrderCategories> DeleteWorkOrderCategories(WorkOrderCategories workOrderCategory);
        IQueryable<WorkOrderCategories> GetAll();
        List<WorkOrderCategories> GetAllWOCategoriesByProjectIds(List<int> projectsIds);
        IQueryable<WorkOrderCategories> GetAllByName(string name);
        IQueryable<WorkOrderCategories> GetAllByIds(IList<int> ids);
        IQueryable<WorkOrderCategories> GetAllByFiltersWithPermisions(PermisionsFilterQueryDto filterQuery);
        WorkOrderCategories GetWorkOrderCategoryRelationshipsById(int id);
        Dictionary<int, string> GetAllKeyValuesByWorkOrderCategoriesCollectionId(int id);
        WorkOrderCategories GetByIdWithSLA(int id);
        IQueryable<WorkOrderCategories> FilterByProject(string text, int?[] selected);
        Dictionary<int, string> GetAllKeyValues();
        IQueryable<WorkOrderCategories> GetByIds(IList<int> ids);
        IQueryable<WorkOrderCategories> FilterByProject(string text, int? selected);
    }
}