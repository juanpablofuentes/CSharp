using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders;

namespace Group.Salto.ServiceLibrary.Common.Contracts.WorkOrder
{
    public interface IWorkOrderCalculateSLADate
    {
        void CalculateSLADates(WorkOrderEditDto workorder);
    }
}