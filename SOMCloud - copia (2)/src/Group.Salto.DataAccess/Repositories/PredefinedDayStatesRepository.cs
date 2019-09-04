using System.Collections.Generic;
using System.Linq;
using DataAccess.Common;
using Group.Salto.Entities;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using Microsoft.EntityFrameworkCore;

namespace Group.Salto.DataAccess.Repositories
{
    public class PredefinedDayStatesRepository : BaseRepository<PredefinedDayStates>, IPredefinedDayStatesRepository
    {

        public PredefinedDayStatesRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<PredefinedDayStates> GetAll()
        {
            return All();
        }

        public IQueryable<PredefinedDayStates> GetByIds(IEnumerable<int> ids)
        {
            return Filter(p => ids.Contains(p.Id)).Include(p => p.PredefinedReasons);
        }
    }
}
