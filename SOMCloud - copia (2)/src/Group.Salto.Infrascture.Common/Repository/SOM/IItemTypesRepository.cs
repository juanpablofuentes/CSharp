using Group.Salto.Entities;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.SOM
{
    public interface IItemTypesRepository : IRepository<ItemTypes>
    {
        IQueryable<ItemTypes> GetAll();
    }
}