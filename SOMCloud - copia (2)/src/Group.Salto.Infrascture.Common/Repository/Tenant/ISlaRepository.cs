using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface ISlaRepository
    {
        Sla GetById(int id);
        IQueryable<Sla> GetAll();
        Dictionary<int, string> GetAllKeyValues();
        SaveResult<Sla> CreateSla(Sla sla);
        SaveResult<Sla> UpdateSla(Sla sla);
        SaveResult<Sla> DeleteSla(Sla entity);
        Sla GetByIdWithStates(int id);
        IQueryable<Sla> GetByReferenceTimeId(int referenceId);
    }
}