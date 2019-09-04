namespace Group.Salto.SOM.Web.Models.ExternalWorkOrderStatus
{
    public class ExternalWorkOrderStatusFilterViewModel : BaseFilter
    {
        public ExternalWorkOrderStatusFilterViewModel()
        {
            OrderBy = "Name";
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public int LanguageId { get; set; }
    }
}