using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class ZonesRepository : BaseRepository<Zones>, IZonesRepository
    {
        public ZonesRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<Zones> GetAllWithZoneProject()
        {
            return All()
                .Include(x => x.ZoneProject)
                .ThenInclude(x=>x.ZoneProjectPostalCode);
        }

        public Zones GetByIdWithZoneProject(int zoneId)
        {
            return Filter(x => x.Id == zoneId)
                .Include(x => x.ZoneProject)
                .ThenInclude(x=>x.Project)
                .Include(x=>x.ZoneProject)
                .ThenInclude(x=>x.ZoneProjectPostalCode)
                .Include(x=>x.PreconditionsLiteralValues)
                .FirstOrDefault();
                
        }

        public Dictionary<int, string> GetAllKeyValues()
        {
            return All()
                .OrderBy(o => o.Name)
                .ToDictionary(t => t.Id, t => t.Name);
        }

        public IQueryable<Zones> GetByIds(IList<int> ids)
        {
            return Filter(x => ids.Contains(x.Id));
        }

        public SaveResult<Zones> CreateZones(Zones zones)
        {
            zones.UpdateDate = DateTime.UtcNow;
            Create(zones);
            var result = SaveChange(zones);
            return result;
        }

        public SaveResult<Zones> UpdateZones(Zones zones)
        {
            zones.UpdateDate = DateTime.UtcNow;
            Update(zones);
            var result = SaveChange(zones);
            return result;
        }

        public SaveResult<Zones> DeleteZone(Zones entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Delete(entity);
            SaveResult<Zones> result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }
    }
}