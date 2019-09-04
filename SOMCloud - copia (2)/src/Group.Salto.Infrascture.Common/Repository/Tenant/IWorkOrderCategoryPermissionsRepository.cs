using Group.Salto.Entities.Tenant;
using System.Collections.Generic;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IWorkOrderCategoryPermissionsRepository: IRepository<WorkOrderCategoryPermissions>
    {
        IList<WorkOrderCategoryPermissions> GetByWorkOrderCategoryId(int workOrderCategoryId);
    }
}