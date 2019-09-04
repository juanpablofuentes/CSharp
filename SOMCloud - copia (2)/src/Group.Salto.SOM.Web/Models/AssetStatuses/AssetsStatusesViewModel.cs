using Group.Salto.Controls.Table.Models;
using Group.Salto.SOM.Web.Models.AssetStatuses;
using System;

namespace Group.Salto.SOM.Web.Models.AssetStatuses
{
    public class AssetsStatusesViewModel
    {
        public MultiItemViewModel<AssetStatusesViewModel, int> AssetStatuses { get; set; }
        public AssetStatusesFilterViewModel AssetStatusesFilters { get; set; }
    }
}