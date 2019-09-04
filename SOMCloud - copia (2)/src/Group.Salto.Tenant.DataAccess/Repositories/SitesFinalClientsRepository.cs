using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class SitesFinalClientsRepository : BaseRepository<LocationsFinalClients>, ISitesFinalClientsRepository
    {
        public SitesFinalClientsRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public List<LocationsFinalClients> GetAllWhereFinalClientId(List<int> IdsToMatch)
        {
            return Filter(y => IdsToMatch.Contains(y.FinalClientId))
                .Include(lfc => lfc.Location).ToList();
        }

        public LocationsFinalClients GetBySiteId(int id)
        {
            return Find(x => x.LocationId == id);
        }

        public SaveResult<LocationsFinalClients> CreateSitesFinalClient(LocationsFinalClients entity)
        {
            Create(entity);
            var result = SaveChange(entity);
            return result;
        }
    }
}