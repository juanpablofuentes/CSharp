using System.Collections.Generic;
using System.Linq;
using DataAccess.Common;
using Group.Salto.Entities;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Group.Salto.DataAccess.Repositories
{
    public class CountryRepository : BaseRepository<Countries>, ICountryRepository
    {
        public CountryRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<Countries> GetAll()
        {
            return IncludeAllCities();
        }

        private IIncludableQueryable<Countries, ICollection<Cities>> IncludeAllCities()
        {
            return DbSet.Include(c => c.Regions)
                .ThenInclude(r => r.States)
                .ThenInclude(r => r.Municipalities)
                .ThenInclude(m => m.Cities);
        }

        public Countries GetById(int id)
        {
            return IncludeAllCities().SingleOrDefault(x=>x.Id == id);
        }
    }
}