namespace Group.Salto.SOM.Web.Models.ClosureCode
{
    public class ClosureCodeFilterViewModel : BaseFilter
    {
        public ClosureCodeFilterViewModel()
        {
            OrderBy = "Name";
        }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}