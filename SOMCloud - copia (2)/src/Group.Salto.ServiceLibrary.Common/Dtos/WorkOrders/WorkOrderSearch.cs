using Group.Salto.Common.Enums;

namespace Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders
{
    public class WorkOrderSearch
    {
        public string SearchString { get; set; }
        public WorkOrderSearchEnum SearchType { get; set; }
    }
}