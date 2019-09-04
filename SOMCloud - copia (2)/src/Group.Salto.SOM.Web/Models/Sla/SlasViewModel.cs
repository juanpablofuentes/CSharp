using Group.Salto.Controls.Table.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Sla
{
    public class SlasViewModel
    {
        public MultiItemViewModel<SlaViewModel, int> Sla { get; set; }

        public SlaFilterViewModel SlaFilters { get; set; }
    }
}