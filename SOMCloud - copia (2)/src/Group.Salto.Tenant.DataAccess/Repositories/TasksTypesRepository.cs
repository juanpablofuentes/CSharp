using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class TasksTypesRepository: BaseRepository<TasksTypes>, ITasksTypesRepository
    {
        public TasksTypesRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<TasksTypes> GetAll()
        {
            return All();
        }
    }
}