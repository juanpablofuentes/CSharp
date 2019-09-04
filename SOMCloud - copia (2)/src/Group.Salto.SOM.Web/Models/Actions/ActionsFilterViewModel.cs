namespace Group.Salto.SOM.Web.Models.Actions
{
    public class ActionsFilterViewModel : BaseFilter
    {
        public ActionsFilterViewModel()
        {
            OrderBy = "Name";
        }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
