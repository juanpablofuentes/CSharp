using DataAccess.Common;
using Group.Salto.Entities;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.DataAccess.Repositories
{
    public class DaysTypeRepository : BaseRepository<DaysType>, IDaysTypeRepository
    {
        public DaysTypeRepository(IUnitOfWork uow) : base(uow)
        {
        }
        public IQueryable<DaysType> GetAll()
        {
            return All();
        }

        public List<string> GetAllNamesById(IList<Guid> queryType_DaysT)
        {
            return Filter(x => queryType_DaysT.Contains(x.Id)).Select(x => x.Name).ToList();
        }

        public DaysType GetById(Guid id)
        {
            return Find(x => x.Id == id);
        }

        public Dictionary<Guid, string> GetAllKeyValues()
        {
            return All()
               .OrderBy(x => x.Id)
               .ToDictionary(t => t.Id, t => t.Name);
        }
    }
}