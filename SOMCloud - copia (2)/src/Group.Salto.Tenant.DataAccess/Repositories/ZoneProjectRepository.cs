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
using System.Text;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class ZoneProjectRepository : BaseRepository<ZoneProject>, IZoneProjectRepository
    {
        public ZoneProjectRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }
        public IQueryable<ZoneProject> GetAllById(int id)
        {
            return Filter(x => x.ZoneId == id)
                .Include(x => x.ZoneProjectPostalCode)
                .Include(x => x.Project)
                .Include(x => x.Zone);
        }

        public IQueryable<ZoneProject> GetPostalcodesByZoneProjectId(int id)
        {
            return Filter(x => x.Id == id)
                .Include(x => x.ZoneProjectPostalCode)
                .Include(x => x.Project)
                .Include(x => x.Zone);
        }

        public SaveResult<ZoneProject> DeleteZoneProject(ZoneProject entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Delete(entity);
            SaveResult<ZoneProject> result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }
    }
}