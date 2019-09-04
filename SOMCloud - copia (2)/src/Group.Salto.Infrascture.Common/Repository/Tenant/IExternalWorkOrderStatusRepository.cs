using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IExternalWorkOrderStatusRepository : IRepository<ExternalWorOrderStatuses>
    {
        IQueryable<ExternalWorOrderStatuses> GetAllWithIncludeTranslations();
        ExternalWorOrderStatuses GetById(int id);
        SaveResult<ExternalWorOrderStatuses> UpdateExternalWorkOrderStatus(ExternalWorOrderStatuses entity);
        SaveResult<ExternalWorOrderStatuses> CreateExternalWorkOrderStatus(ExternalWorOrderStatuses entity);
        SaveResult<ExternalWorOrderStatuses> DeleteExternalWorkOrderStatus(ExternalWorOrderStatuses entity);
        ExternalWorOrderStatuses GetByIdWithWorkOrders(int id);
        IQueryable<ExternalWorOrderStatuses> GetByIds(IList<int> ids);
    }
}