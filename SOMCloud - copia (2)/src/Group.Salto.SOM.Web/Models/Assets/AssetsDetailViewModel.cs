using Group.Salto.SOM.Web.Models.AssetsAudit;
using Group.Salto.SOM.Web.Models.MultiCombo;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Assets
{
    public class AssetsDetailViewModel
    {
        public AssetsDetailViewModel()
        {
            GenericDetailViewModel = new GenericDetailViewModel();
            AssetsAuditViewModel = new List<AssetsAuditViewModel>();
        }

        public GenericDetailViewModel GenericDetailViewModel { get; set; }
        public SecondaryDetailViewModel SecondaryDetailViewModel { get; set; }
        public IList<AssetsAuditViewModel> AssetsAuditViewModel { get; set; }
        public int UserId { get; set; }

        private int? fromSiteId;
        public int? FromSiteId
        {
            get { return this.fromSiteId; }
            set
            {
                if (value != null) this.fromSiteId = value;
                else this.fromSiteId = 0;
            }
        }
    }
}