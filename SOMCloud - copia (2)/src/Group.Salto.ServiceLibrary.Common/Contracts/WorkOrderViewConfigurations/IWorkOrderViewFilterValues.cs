using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders;

namespace Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderViewConfigurations
{
    public interface IWorkOrderViewFilterValues
    {
        WorkOrderFiltersDto GetFilterValues(UsersMainWoviewConfigurations data);
    }
}