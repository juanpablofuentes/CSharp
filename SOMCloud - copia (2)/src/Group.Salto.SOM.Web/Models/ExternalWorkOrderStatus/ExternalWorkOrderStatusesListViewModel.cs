using Group.Salto.Controls.Table.Models;

namespace Group.Salto.SOM.Web.Models.ExternalWorkOrderStatus
{
    public class ExternalWorkOrderStatusesListViewModel
    {
        public MultiItemViewModel<ExternalWorkOrderStatusListViewModel, int> ExternalWorkOrderStatusList { get; set; }
        public ExternalWorkOrderStatusFilterViewModel ExternalWorkOrderStatusFilter { get; set; }
    }
}