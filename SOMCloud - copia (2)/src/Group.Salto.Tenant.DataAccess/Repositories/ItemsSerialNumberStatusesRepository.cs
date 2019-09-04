using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class ItemsSerialNumberStatusesRepository : BaseRepository<ItemsSerialNumberStatuses>, IItemsSerialNumberStatusesRepository
    {
        public ItemsSerialNumberStatusesRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public Dictionary<int, string> GetAllKeyValues()
        {
            return All()
                .OrderBy(x => x.Name)
                .ToDictionary(t => t.Id, t => t.Name);
        }
    }
}