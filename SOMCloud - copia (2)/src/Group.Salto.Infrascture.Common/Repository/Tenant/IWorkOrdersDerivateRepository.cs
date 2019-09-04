using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using System.Collections.Generic;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IWorkOrdersDerivateRepository : IRepository<WorkOrdersDeritative>
    {
        WorkOrdersDeritative GetById(int id);
        WorkOrdersDeritative GetEditById(int id);
        SaveResult<WorkOrdersDeritative> CreateWorkOrderDerivated(WorkOrdersDeritative entity);
        SaveResult<WorkOrdersDeritative> UpdateWorkOrderDerivated(WorkOrdersDeritative entity);
        IList<WorkOrdersDeritative> GetByTaskId(int id);
    }
}