namespace Group.Salto.SOM.Web.Models.Knowledge
{
    public class KnowledgesFilterViewModel : BaseFilter
    {
        public KnowledgesFilterViewModel()
        {
            OrderBy = "Name";
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Observations { get; set; }
    }
}
