using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Zones
{
    public class ZonesFilterViewModel : BaseFilter
    {
        public ZonesFilterViewModel()
        {
            OrderBy = "Name";
        }

        public string Name { get; set; }
    }
}