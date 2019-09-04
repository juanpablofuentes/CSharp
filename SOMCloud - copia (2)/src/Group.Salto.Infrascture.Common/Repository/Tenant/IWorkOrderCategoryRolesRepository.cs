using Group.Salto.Entities.Tenant;
using System.Collections.Generic;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IWorkOrderCategoryRolesRepository : IRepository<WorkOrderCategoryRoles>
    {
        IList<WorkOrderCategoryRoles> GetByWorkOrderCategoryId(int workOrderCategoryId);
    }
}