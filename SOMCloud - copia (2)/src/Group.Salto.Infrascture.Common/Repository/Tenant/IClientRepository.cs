using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IClientRepository : IRepository<Clients>
    {
        Clients GetById(int id);
        IQueryable<Clients> GetAll();
        Dictionary<int, string> GetAllKeyValues();
        SaveResult<Clients> CreateClients(Clients client);
        SaveResult<Clients> UpdateClients(Clients client);
        SaveResult<Clients> DeleteClients(Clients client);
        IQueryable<Clients> GetAllByFiltersWithPermisions(PermisionsFilterQueryDto filterQuery);
        IQueryable<Clients> GetByIds(IList<int> ids);
    }
}