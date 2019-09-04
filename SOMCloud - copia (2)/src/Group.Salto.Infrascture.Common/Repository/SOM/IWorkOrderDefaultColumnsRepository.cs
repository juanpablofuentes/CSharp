using Group.Salto.Entities;
using System.Collections.Generic;

namespace Group.Salto.Infrastructure.Common.Repository.SOM
{
    public interface IWorkOrderDefaultColumnsRepository
    {
        IEnumerable<WorkOrderDefaultColumns> GetAll();
    }
}