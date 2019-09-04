using Group.Salto.Common;
using Group.Salto.Entities;
using Group.Salto.Infrastructure.Common;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.DataAccess.Repositories
{
    public interface IOriginsRepository : IRepository<Origins>
    {
        IQueryable<Origins> GetAllNotDeleted();
        Origins GetByIdNotDeleted(int id);
        SaveResult<Origins> CreateOrigin(Origins origins);
        SaveResult<Origins> UpdateOrigin(Origins origins);
        bool DeleteOrigin(Origins origins);
        Dictionary<int, string> GetAllOriginKeyValues();

    }
}