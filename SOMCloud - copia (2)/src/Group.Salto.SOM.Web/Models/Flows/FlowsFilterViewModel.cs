namespace Group.Salto.SOM.Web.Models.Flows
{
    public class FlowsFilterViewModel : BaseFilter
    {
        public FlowsFilterViewModel()
        {
            OrderBy = "Name";
        }

        public string Name { get; set; }
    }
}