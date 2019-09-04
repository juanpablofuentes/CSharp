using DataAccess.Common;
using Group.Salto.Entities;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using System;
using System.Linq;

namespace Group.Salto.DataAccess.Repositories
{
    public class PreconditionTypesRepository : BaseRepository<PreconditionTypes>, IPreconditionTypesRepository
    {
        public PreconditionTypesRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<PreconditionTypes> GetAll()
        {
            return All();
        }

        public PreconditionTypes GetById(Guid id)
        {
            return Find(x => x.Id == id);
        }

        public PreconditionTypes GetByName(string name)
        {
            return Find(x => x.Name == name);
        }
    }
}