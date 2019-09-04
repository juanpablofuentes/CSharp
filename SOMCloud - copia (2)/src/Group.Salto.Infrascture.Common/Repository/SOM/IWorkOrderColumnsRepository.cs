using Group.Salto.Entities;
using System.Collections.Generic;

namespace Group.Salto.Infrastructure.Common.Repository.SOM
{
    public interface IWorkOrderColumnsRepository : IRepository<WorkOrderColumns>
    {
        List<WorkOrderColumns> GetAllColumns();
    }
}