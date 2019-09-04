using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface ISymptomRepository : IRepository<Symptom>
    {
        IQueryable<Symptom> GetAll();
        Symptom GetById(int id);
        SaveResult<Symptom> CreateSymptom(Symptom entity);
        SaveResult<Symptom> UpdateSymptom(Symptom entity);
        SaveResult<Symptom> DeleteSymptom(Symptom entity);
        IQueryable<Symptom> GetAllById(IEnumerable<int> ids);
        Dictionary<int, string> GetOrphansKeyValue();
    }
}