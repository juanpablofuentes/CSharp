using Group.Salto.Controls.Table.Models;

namespace Group.Salto.SOM.Web.Models.WorkOrderCategoriesCollection
{
    public class WorkOrderCategoriesCollectionListViewModel
    {
        public MultiItemViewModel<WorkOrderCategoriesCollectionBaseViewModel, int> WorkOrderCategoriesCollectionList { get; set; }
        public WorkOrderCategoriesCollectionFilterViewModel Filters { get; set; }
    }
}