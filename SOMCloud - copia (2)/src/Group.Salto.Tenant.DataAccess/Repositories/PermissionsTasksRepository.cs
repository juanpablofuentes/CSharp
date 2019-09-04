using DataAccess.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Common.UoW;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class PermissionsTasksRepository: BaseRepository<PermissionsTasks>, IPermissionsTasksRepository
    {
        public PermissionsTasksRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public IList<PermissionsTasks> GetAllPermissionsByTaskId(int taskId)
        {
            return Filter(x => x.TaskId == taskId).ToList();
        }

        public PermissionsTasks AddPermission(PermissionsTasks permission)
        {
            return Create(permission);  
        }

        public PermissionsTasks DeleteOnContext(PermissionsTasks entity)
        {
            Delete(entity);
            return entity;
        }
    }
}