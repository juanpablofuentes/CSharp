using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface ISymptomCollectionRepository : IRepository<SymptomCollections>
    {
        IQueryable<SymptomCollections> GetAll();
        SymptomCollections GetById(int id);
        SaveResult<SymptomCollections> CreateSymptomCollection(SymptomCollections entity);
        SaveResult<SymptomCollections> UpdateSymptomCollection(SymptomCollections entity);
        SaveResult<SymptomCollections> DeleteSymptomCollection(SymptomCollections entity);
        Dictionary<int, string> GetAllKeyValues();
    }
}