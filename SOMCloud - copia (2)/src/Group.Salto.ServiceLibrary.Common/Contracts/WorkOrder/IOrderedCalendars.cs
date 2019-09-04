using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders;

namespace Group.Salto.ServiceLibrary.Common.Contracts.WorkOrder
{
    public interface IOrderedCalendars
    {
        OrderedCalendarsDto NewOrderedCalendars(OrderedCalendarsDto parameter);
    }
}