namespace Group.Salto.SOM.Web.Models.EventCategories
{
    public class EventCategoriesFilterViewModel : BaseFilter
    {
        public EventCategoriesFilterViewModel()
        {
            OrderBy = "Name";
        }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
