using DataAccess.Common;
using Group.Salto.Entities;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.DataAccess.Repositories
{
    public class WorkOrderColumnsRepository : BaseRepository<WorkOrderColumns>, IWorkOrderColumnsRepository
    {
        public WorkOrderColumnsRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public List<WorkOrderColumns> GetAllColumns()
        {
            return All().ToList();
        }
    }
}