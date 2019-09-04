using Group.Salto.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.SOM
{
    public interface IActionGroupRepository
    {
        Dictionary<Guid, string> GetAllKeyValues();
        IQueryable<ActionGroups> GetAllByIds(IList<Guid> ids);
    }
}