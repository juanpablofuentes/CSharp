using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common;
using Group.Salto.Entities;

namespace Group.Salto.Infrastructure.Common.Repository.SOM
{
    public interface IActionRepository : IRepository<Actions>
    {
        Actions FindById(int id);
        SaveResult<Actions> UpdateAction(Actions actions);
        IQueryable<Actions> GetAll();
        Dictionary<int, string> GetAllKeyValues();
    }
}