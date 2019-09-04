using Group.Salto.Entities.Tenant;
using System.Collections.Generic;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IPermissionsTasksRepository
    {
        IList<PermissionsTasks> GetAllPermissionsByTaskId(int taskId);
        PermissionsTasks AddPermission(PermissionsTasks permission);
        PermissionsTasks DeleteOnContext(PermissionsTasks entity);
    }
}