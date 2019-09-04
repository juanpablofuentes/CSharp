namespace Group.Salto.SOM.Web.Models.SymptomCollection
{
    public class SymptomCollectionFilterViewModel : BaseFilter
    {
        public SymptomCollectionFilterViewModel()
        {
            OrderBy = "Name";
        }

        public string Name { get; set; }
        public string Element { get; set; }
    }
}