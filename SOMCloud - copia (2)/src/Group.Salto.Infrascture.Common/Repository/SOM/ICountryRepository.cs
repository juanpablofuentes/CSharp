using System.Linq;
using Group.Salto.Entities;

namespace Group.Salto.Infrastructure.Common.Repository.SOM
{
    public interface ICountryRepository : IRepository<Countries>
    {
        IQueryable<Countries> GetAll();
        Countries GetById(int id);
    }
}