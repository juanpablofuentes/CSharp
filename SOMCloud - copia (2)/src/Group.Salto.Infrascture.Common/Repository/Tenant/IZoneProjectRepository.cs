using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IZoneProjectRepository
    {
        IQueryable<ZoneProject> GetAllById(int id);
        IQueryable<ZoneProject> GetPostalcodesByZoneProjectId(int id);
        SaveResult<ZoneProject> DeleteZoneProject(ZoneProject entity);
    }
}
