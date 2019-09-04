using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface ITaskRepository : IRepository<Tasks>
    {
        IQueryable<Tasks> GetAll();
        IQueryable<Tasks> GetAllById(IList<int> ids);
        Tasks GetByIdWithIncludeTranslations(int id);
        IQueryable<Tasks> GetAvailableTasksFromWoId(People people, WorkOrders woId, IEnumerable<int> woTypes);
        Tasks GetByIdIncludeBasicInfo(int taskId);
        IQueryable<PostconditionCollections> GetAvailablePostConditionCollection(int taskId, People people, WorkOrders wo, IEnumerable<int> woTypes);
        Dictionary<int, string> GetTasksIdNameByFlowId(int id);
        Tasks GetById(int taskId);
        IQueryable<Tasks> GetWorkOrderCreationTask(People people, WorkOrders workOrder, IEnumerable<int> woTypes);
        SaveResult<Tasks> UpdateTask(Tasks task);
        SaveResult<Tasks> CreateTask(Tasks entity);
        IQueryable<Tasks> GetTasksByFlowIdIncludeTranslations(int id);
    }
}