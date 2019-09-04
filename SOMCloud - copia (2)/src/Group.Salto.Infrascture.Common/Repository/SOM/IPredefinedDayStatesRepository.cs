using System.Collections.Generic;
using System.Linq;
using Group.Salto.Entities;

namespace Group.Salto.Infrastructure.Common.Repository.SOM
{
    public interface IPredefinedDayStatesRepository
    {
        IQueryable<PredefinedDayStates> GetAll();
        IQueryable<PredefinedDayStates> GetByIds(IEnumerable<int> ids);
    }
}
