using System.Linq;
using DataAccess.Common;
using Group.Salto.Entities;
using Group.Salto.Infrastructure.Common.Repository.SOM;

namespace Group.Salto.DataAccess.Repositories
{
    public class ItemTypesRepository : BaseRepository<ItemTypes>, IItemTypesRepository
    {
        public ItemTypesRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<ItemTypes> GetAll()
        {
            return All();
        }
    }
}