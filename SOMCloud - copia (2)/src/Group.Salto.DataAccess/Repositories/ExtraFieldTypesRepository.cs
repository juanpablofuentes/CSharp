using DataAccess.Common;
using Group.Salto.Entities;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.DataAccess.Repositories
{
    public class ExtraFieldTypesRepository : BaseRepository<ExtraFieldTypes>, IExtraFieldTypesRepository
    {
        public ExtraFieldTypesRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public ExtraFieldTypes GetById(int id)
        {
            return Find(x => x.Id == id);
        }

        public IQueryable<ExtraFieldTypes> GetAll()
        {
            return All();
        }

        public Dictionary<int, string> GetAllKeyValues()
        {
            return All()
                .OrderBy(x => x.Name)
                .ToDictionary(t => t.Id, t => t.Name);
        }
    }
}