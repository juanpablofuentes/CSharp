using Group.Salto.Common.Enums;

namespace Group.Salto.SOM.Web.Models.WorkOrder
{
    public class WorkOrderSearch
    {
        public string SearchString { get; set; } 
        public WorkOrderSearchEnum SearchType { get; set; }
    }
}