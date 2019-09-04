using DataAccess.Common;
using Group.Salto.Entities;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.DataAccess.Repositories
{
    public class ContractTypeRepository : BaseRepository<ContractType>, IContractTypeRepository
    {
        public ContractTypeRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public Dictionary<int, string> GetAllKeyValues()
        {
            return All()
                .Select(s => new { s.Id, s.Value })
                .OrderBy(o => o.Value)
                .ToDictionary(t => t.Id, t => t.Value);
        }
    }
}