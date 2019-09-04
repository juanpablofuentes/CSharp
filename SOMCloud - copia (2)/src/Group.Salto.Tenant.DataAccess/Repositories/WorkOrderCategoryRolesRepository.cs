using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class WorkOrderCategoryRolesRepository : BaseRepository<WorkOrderCategoryRoles>, IWorkOrderCategoryRolesRepository
    {
        public WorkOrderCategoryRolesRepository(ITenantUnitOfWork uow) : base(uow)
        {

        }

        public IList<WorkOrderCategoryRoles> GetByWorkOrderCategoryId(int workOrderCategoryId)
        {
            return Filter(x => x.WorkOrderCategoryId == workOrderCategoryId).ToList();
        }
    }
}