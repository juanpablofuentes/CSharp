using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface ISitesFinalClientsRepository
    {
        SaveResult<LocationsFinalClients> CreateSitesFinalClient(LocationsFinalClients entity);
        LocationsFinalClients GetBySiteId(int id);
        List<LocationsFinalClients> GetAllWhereFinalClientId(List<int> IdsToMatch);
    }
}