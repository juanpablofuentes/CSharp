using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class ErpSystemInstanceRepository: BaseRepository<ErpSystemInstance>, IErpSystemInstanceRepository
    { 
        public ErpSystemInstanceRepository(ITenantUnitOfWork uow) : base(uow) { }

        public Dictionary<int, string> GetAllKeyValues() 
        {
            return All()
                .OrderBy(o => o.Name)
                .ToDictionary(t => t.Id, t => t.Name);
        }
    }
}