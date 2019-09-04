using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IStatesSlaRepository
    {
        IQueryable<StatesSla> GetAll();
        IQueryable<StatesSla> GetAllBySlaId(int id);
        Dictionary<int, string> GetAllKeyValues();
        SaveResult<StatesSla> CreateStatesSla(StatesSla sla);
        SaveResult<StatesSla> UpdateStatesSla(StatesSla statesla);
        SaveResult<StatesSla> DeleteStatesSla(StatesSla entity);
        StatesSla GetById(int id);
        StatesSla DeleteOnContextStatesSla(StatesSla entity);
        string GetColor(int minutes, int slaId);
    }
}