using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IContractsRepository : IRepository<Contracts>
    {
        IQueryable<Contracts> GetAll();
        Contracts GetById(int Id);
        SaveResult<Contracts> CreateContracts(Contracts contracts);
        SaveResult<Contracts> UpdateContracts(Contracts contracts);
        bool DeleteContracts(Contracts contracts);
        Dictionary<int, string> GetAllKeyValues();
    }
}