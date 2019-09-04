namespace Group.Salto.SOM.Web.Models.WorkOrderStatus
{
    public class WorkOrderStatusFilterViewModel : BaseFilter
    {
        public WorkOrderStatusFilterViewModel()
        {
            OrderBy = "Name";
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public int LanguageId { get; set; }
    }
}