namespace Group.Salto.SOM.Web.Models.PeopleCollection
{
    public class PeopleCollectionFilterViewModel : BaseFilter
    {
        public PeopleCollectionFilterViewModel()
        {
            OrderBy = "Name";
        }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}