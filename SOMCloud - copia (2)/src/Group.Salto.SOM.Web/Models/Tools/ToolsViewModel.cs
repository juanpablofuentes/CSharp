using Group.Salto.Controls.Table.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Tools
{
    public class ToolsViewModel
    {
        public MultiItemViewModel<ToolViewModel, int> Tools { get; set; }

        public ToolsFilterViewModel ToolsFilters { get; set; }
    }
}
