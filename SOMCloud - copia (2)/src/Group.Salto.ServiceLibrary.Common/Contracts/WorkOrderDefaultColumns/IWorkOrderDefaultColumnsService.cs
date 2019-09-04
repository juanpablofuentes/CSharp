using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderDefaultColumns
{
    public interface IWorkOrderDefaultColumnsService
    {
        IEnumerable<Entities.WorkOrderDefaultColumns> GetAll();
    }
}