using Group.Salto.Controls.Table.Models;

namespace Group.Salto.SOM.Web.Models.WorkOrderCategory
{
    public class WorkOrderCategoryListViewModel
    {
        public MultiItemViewModel<WorkOrderCategoryBaseViewModel, int> WorkOrderCategoriesList { get; set; }
        public WorkOrderCategoryFilterViewModel Filters { get; set; }
    }
}