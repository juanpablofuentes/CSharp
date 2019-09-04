using Group.Salto.Controls.Table.Models;

namespace Group.Salto.SOM.Web.Models.CollectionTypeWorkOrders
{
    public class CollectionTypeWorkOrdersListViewModel
    {
        public MultiItemViewModel<CollectionTypeWorkOrdersViewModel, int> CollectionTypesWorkOrders { get; set; }
        public CollectionTypeWorkOrdersFilterViewModel Filters { get; set; }
    }
}