using Group.Salto.Controls.Table.Models;

namespace Group.Salto.SOM.Web.Models.Flows
{
    public class FlowsListViewModel
    {
        public MultiItemViewModel<FlowsViewModel, int> FlowsList { get; set; }
        public FlowsFilterViewModel Filters { get; set; }
    }
}