using Group.Salto.Entities.Tenant;
using System.Collections.Generic;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IErpSystemInstanceRepository : IRepository<ErpSystemInstance>
    {
        Dictionary<int, string> GetAllKeyValues();
    }
}