using Group.Salto.Entities.Tenant;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IPermissionsRepository
    {
        Dictionary<int, string> GetAllKeyValues();
        IQueryable<Permissions> GetAllById(IEnumerable<int> ids);
        IQueryable<Permissions> GetAllNotDeleted();
        Permissions GetByIdNotDeleted(int id);
        SaveResult<Permissions> UpdatePermissions(Permissions entity);
        SaveResult<Permissions> CreatePermissions(Permissions entity);
        SaveResult<Permissions> DeletePermissions(Permissions entity);
    }
}