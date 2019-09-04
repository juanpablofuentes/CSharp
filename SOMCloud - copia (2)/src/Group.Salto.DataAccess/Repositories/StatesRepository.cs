using DataAccess.Common;
using Group.Salto.Entities;
using Group.Salto.Infrastructure.Common.Dto;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.DataAccess.Repositories
{
    public class StatesRepository : BaseRepository<States>, IStatesRepository
    {
        public StatesRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<States> GetAll()
        {
            return All();
        }

        public IQueryable<States> GetAllByFilters(StateFilterRepositoryDto filterQuery)
        {
            IQueryable<States> query = All();
            query = FilterQuery(filterQuery, query);
            return query;
        }

        private IQueryable<States> FilterQuery(StateFilterRepositoryDto filterQuery, IQueryable<States> query)
        {
            if (filterQuery.Ids != null)
            {
                query = query.Where(x => filterQuery.Ids.Contains(x.Id));
            }

            if (!string.IsNullOrEmpty(filterQuery.Name))
            {
                query = query.Where(x => x.Name.Contains(filterQuery.Name));
            }
            return query;
        }

        public Dictionary<int, string> GetAllKeyValues()
        {
            return All()
                .OrderBy(o => o.Name)
                .ToDictionary(t => t.Id, t => t.Name);
        }

        public IQueryable<States> GetByIds(IList<int> ids)
        {
            return Filter(x => ids.Contains(x.Id));
        }
    }
}