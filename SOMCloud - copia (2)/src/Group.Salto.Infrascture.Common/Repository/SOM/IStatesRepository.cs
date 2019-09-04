using Group.Salto.Entities;
using Group.Salto.Infrastructure.Common.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.SOM
{
    public interface IStatesRepository : IRepository<States>
    {
        IQueryable<States> GetAll();
        IQueryable<States> GetAllByFilters(StateFilterRepositoryDto filterQuery);
        Dictionary<int, string> GetAllKeyValues();
        IQueryable<States> GetByIds(IList<int> ids);
    }
}