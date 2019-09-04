using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IZonesRepository
    {
        Dictionary<int, string> GetAllKeyValues();
        IQueryable<Zones> GetByIds(IList<int> ids);
        Zones GetByIdWithZoneProject(int zoneId);
        IQueryable<Zones> GetAllWithZoneProject();
        SaveResult<Zones> CreateZones(Zones zones);
        SaveResult<Zones> UpdateZones(Zones zones);
        SaveResult<Zones> DeleteZone(Zones entity);
    }
}