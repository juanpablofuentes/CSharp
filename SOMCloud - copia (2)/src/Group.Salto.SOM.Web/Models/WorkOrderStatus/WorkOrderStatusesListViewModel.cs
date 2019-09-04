using Group.Salto.Controls.Table.Models;

namespace Group.Salto.SOM.Web.Models.WorkOrderStatus
{
    public class WorkOrderStatusesListViewModel
    {
        public MultiItemViewModel<WorkOrderStatusListViewModel, int> WorkOrderStatusList { get; set; }
        public WorkOrderStatusFilterViewModel WorkOrderStatusFilter { get; set; }
    }
}