namespace Group.Salto.SOM.Web.Models.SiteUser
{
    public class SiteUserFilterViewModel : BaseFilter
    {
        public SiteUserFilterViewModel()
        {
            OrderBy = "Name";
        }
        
        public string Name { get; set; }
        public int SitesId { get; set; }
        public string SitesName { get; set; }
    }
}