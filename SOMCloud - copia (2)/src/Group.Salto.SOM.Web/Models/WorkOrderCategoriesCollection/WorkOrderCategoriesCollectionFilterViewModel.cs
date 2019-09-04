namespace Group.Salto.SOM.Web.Models.WorkOrderCategoriesCollection
{
    public class WorkOrderCategoriesCollectionFilterViewModel : BaseFilter
    {
        public WorkOrderCategoriesCollectionFilterViewModel()
        {
            OrderBy = "Name";
        }

        public string Name { get; set; }
        public string Info { get; set; }
    }
}