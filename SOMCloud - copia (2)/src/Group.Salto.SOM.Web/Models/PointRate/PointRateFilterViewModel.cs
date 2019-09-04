using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.PointRate
{
    public class PointRateFilterViewModel : BaseFilter
    {
        public PointRateFilterViewModel()
        {
            OrderBy = "Name";
        }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
