using Group.Salto.Entities.Tenant;
using System.Linq;
using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IWorkOrderStatusRepository : IRepository<WorkOrderStatuses>
    {
        IQueryable<WorkOrderStatuses> GetAllWithIncludeTranslations();
        WorkOrderStatuses GetById(int id);
        WorkOrderStatuses GetByIdWithWorkOrders(int id);
        IQueryable<WorkOrderStatuses> GetByIds(IList<int> ids);
        SaveResult<WorkOrderStatuses> CreateWorkOrderStatus(WorkOrderStatuses entity);
        SaveResult<WorkOrderStatuses> UpdateWorkOrderStatus(WorkOrderStatuses entity);
        SaveResult<WorkOrderStatuses> DeleteWorkOrderStatus(WorkOrderStatuses entity);
    }
}