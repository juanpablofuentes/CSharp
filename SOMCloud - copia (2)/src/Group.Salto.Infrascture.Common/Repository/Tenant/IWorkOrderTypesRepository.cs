using Group.Salto.Entities.Tenant;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IWorkOrderTypesRepository : IRepository<WorkOrderTypes>
    {
        WorkOrderTypes GetById(int id);
        WorkOrderTypes GetByIdIncludingFather(int id);
        IQueryable<WorkOrderTypes> GetAllByWorkOrderTypesFatherId(int id);
        List<WorkOrderTypes> GetByCollectionsTypesWorkOrdersId(int id);
        WorkOrderTypes DeleteOnContext(WorkOrderTypes entity);
        List<WorkOrderTypes> GetAllByWorkOrderTypesByFathersIds(List<int?> IdsToMatch);
        Dictionary<int, string> GetAllKeyValues();
        IQueryable<WorkOrderTypes> GetByIds(IList<int> ids);
    }
}