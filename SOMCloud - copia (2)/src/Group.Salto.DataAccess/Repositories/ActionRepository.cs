using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Common;
using Group.Salto.Common;
using Group.Salto.Entities;
using Group.Salto.Infrastructure.Common.Repository.SOM;

namespace Group.Salto.DataAccess.Repositories
{
    public class ActionRepository : BaseRepository<Actions>, IActionRepository
    {
        public ActionRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public Actions FindById(int id)
        {
            return Find(x => x.Id == id);
        }

        public SaveResult<Actions> UpdateAction(Actions actions)
        {
            actions.UpdateDate = DateTime.UtcNow;
            Update(actions);
            var result = SaveChange(actions);
            return result;
        }

        public IQueryable<Actions> GetAll()
        {
            return All();
        }

        public Dictionary<int, string> GetAllKeyValues()
        {
            return All()
                .Select(s => new { s.Id, s.Name })
                .ToDictionary(t => t.Id, t => t.Name);
        }
    }
}
