using Group.Salto.Controls.Table.Models;
using System;

namespace Group.Salto.SOM.Web.Models.Origins
{
    public class OriginsListViewModel
    {
        public MultiItemViewModel<OriginListViewModel, int> Origins { get; set; }

        public OriginsFilterViewModel OriginFilter { get; set; }
    }
}
