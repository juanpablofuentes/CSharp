using DataAccess.Common;
using Group.Salto.Entities;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using System.Linq;

namespace Group.Salto.DataAccess.Repositories
{
    public class PostconditionTypesRepository : BaseRepository<PostconditionTypes>, IPostconditionTypesRepository
    {
        public PostconditionTypesRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<PostconditionTypes> GetAll()
        {
            return All();
        }

        public PostconditionTypes GetByName(string name)
        {
            return Find(x => x.Name == name);
        }
    }
}