using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Sla
{
    public class SlaFilterViewModel : BaseFilter
    {
        public SlaFilterViewModel()
        {
            OrderBy = "Name";
        }

        public string Name { get; set; }
    }
}