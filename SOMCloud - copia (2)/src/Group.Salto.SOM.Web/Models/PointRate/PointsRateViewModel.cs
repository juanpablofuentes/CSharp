using Group.Salto.Controls.Table.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.PointRate
{
    public class PointsRateViewModel
    {
        public MultiItemViewModel<PointRateViewModel, int> PointRate { get; set; }

        public PointRateFilterViewModel PointRateFilters { get; set; }
    }
}
