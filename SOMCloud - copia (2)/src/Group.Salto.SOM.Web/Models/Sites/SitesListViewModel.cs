using Group.Salto.Controls.Table.Models;

namespace Group.Salto.SOM.Web.Models.Sites
{
    public class SitesListViewModel
    {
        public MultiItemViewModel<SitesViewModel, int> SitesList { get; set; }
        public SitesFilterViewModel Filters { get; set; }
    }
}