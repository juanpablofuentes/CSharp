namespace Group.Salto.SOM.Web.Models
{
    public class WorkCenterFilterViewModel : BaseFilter
    {
        public WorkCenterFilterViewModel()
        {
            OrderBy = "Name";
        }

        public string Name { get; set; }
    }
}