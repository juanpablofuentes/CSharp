using Group.Salto.Controls.Table.Models;

namespace Group.Salto.SOM.Web.Models.Assets
{
    public class AssetsListViewModel
    {
        public int? FinalClientsId { get; set; }
        public int? FromSiteId { get; set; }
        public MultiItemViewModel<AssetsViewModel, int> AssetsList { get; set; }
        public AssetsFilterViewModel Filters { get; set; }
        public AssetsTransferViewModel Transfer { get; set; }
    }
}