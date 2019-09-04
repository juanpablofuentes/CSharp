namespace Group.Salto.SOM.Web.Models.CollectionsExtraField
{
    public class CollectionsExtraFieldFilterViewModel : BaseFilter
    {
        public CollectionsExtraFieldFilterViewModel()
        {
            OrderBy = "Name";
        }

        public string Name { get; set; }
    }
}