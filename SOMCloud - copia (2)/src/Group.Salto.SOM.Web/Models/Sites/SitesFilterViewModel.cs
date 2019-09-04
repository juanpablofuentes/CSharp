namespace Group.Salto.SOM.Web.Models.Sites
{
    public class SitesFilterViewModel : BaseFilter
    {
        public SitesFilterViewModel()
        {
            OrderBy = "Name";
        }

        public string Name { get; set; }
        public int FinalClientId { get; set; }
        public string FinalClientName { get; set; }
    }
}