using Group.Salto.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.SOM
{
    public interface IMunicipalityRepository : IRepository<Municipalities>
    {
        Municipalities GetById(int id);
        Dictionary<int, string> GetAllKeyValues();
        Municipalities GetByIdWithStatesRegionsCountriesIncludes(int id);
    }
}