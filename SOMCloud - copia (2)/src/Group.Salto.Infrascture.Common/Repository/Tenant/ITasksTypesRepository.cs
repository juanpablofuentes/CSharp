using Group.Salto.Entities.Tenant;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface ITasksTypesRepository
    {
        IQueryable<TasksTypes> GetAll();
    }
}