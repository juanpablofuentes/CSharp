namespace Group.Salto.SOM.Web.Models.WorkOrderCategory
{
    public class WorkOrderCategoryFilterViewModel : BaseFilter
    {
        public WorkOrderCategoryFilterViewModel()
        {
            OrderBy = "Name";
        }

        public string Name { get; set; }
        public string Info { get; set; }
        public string EstimatedDuration { get; set; }
    }
}