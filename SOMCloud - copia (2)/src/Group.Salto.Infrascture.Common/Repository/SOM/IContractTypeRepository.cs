using Group.Salto.Entities;
using System.Collections.Generic;

namespace Group.Salto.Infrastructure.Common.Repository.SOM
{
    public interface IContractTypeRepository : IRepository<ContractType>
    {
        Dictionary<int, string> GetAllKeyValues();
    }
}