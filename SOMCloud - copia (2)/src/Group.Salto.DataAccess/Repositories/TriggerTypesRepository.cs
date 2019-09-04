using DataAccess.Common;
using Group.Salto.Entities;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using System;
using System.Linq;

namespace Group.Salto.DataAccess.Repositories
{
    public class TriggerTypesRepository : BaseRepository<TriggerTypes>, ITriggerTypesRepository
    {
        public TriggerTypesRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<TriggerTypes> GetAll()
        {
            return All();
        }

        public TriggerTypes GetById(Guid id)
        {
            return Filter(x => x.Id == id).SingleOrDefault();
        }

        public TriggerTypes GetByName(string name)
        {
            return Find(x => x.Name == name);
        }
    }
}