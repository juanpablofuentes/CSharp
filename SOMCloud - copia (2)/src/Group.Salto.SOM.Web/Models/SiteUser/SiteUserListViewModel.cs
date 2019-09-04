using Group.Salto.Controls.Table.Models;

namespace Group.Salto.SOM.Web.Models.SiteUser
{
    public class SiteUserListViewModel
    {
        public MultiItemViewModel<SiteUserViewModel, int> SiteUserList { get; set; }
        public SiteUserFilterViewModel Filters { get; set; }
        public int FinalClientsId { get; set; }
    }
}