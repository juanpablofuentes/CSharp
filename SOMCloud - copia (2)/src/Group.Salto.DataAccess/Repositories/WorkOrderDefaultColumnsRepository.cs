using DataAccess.Common;
using Group.Salto.Entities;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.DataAccess.Repositories
{
    public class WorkOrderDefaultColumnsRepository : BaseRepository<WorkOrderDefaultColumns>, IWorkOrderDefaultColumnsRepository
    {
        public WorkOrderDefaultColumnsRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public IEnumerable<WorkOrderDefaultColumns> GetAll()
        {
            return All().OrderBy(x => x.Position).ToList();
        }
    }
}