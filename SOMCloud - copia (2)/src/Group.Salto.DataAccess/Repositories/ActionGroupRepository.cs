using DataAccess.Common;
using Group.Salto.Entities;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.DataAccess.Repositories
{
    public class ActionGroupRepository : BaseRepository<ActionGroups>, IActionGroupRepository
    {
        public ActionGroupRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public Dictionary<Guid, string> GetAllKeyValues()
        {
            return All()
                .Select(s => new { s.Id, s.Name })
                .ToDictionary(t => t.Id, t => t.Name);
        }

        public IQueryable<ActionGroups> GetAllByIds(IList<Guid> ids)
        {
            return Filter(x => ids.Any(k => k == x.Id));
        }
    }
}