using Group.Salto.Controls.Table.Models;
using System;

namespace Group.Salto.SOM.Web.Models.AssetStatuses
{
    public class AssetStatusesFilterViewModel : BaseFilter
    {
        public AssetStatusesFilterViewModel()
        {
            OrderBy = "Name";
        }

        public string Name { get; set; }
        public bool IsRetiredState { get; set; }
    }
}