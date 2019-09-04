using Group.Salto.Controls.Table.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.ToolsType
{
    public class ToolsTypeViewModel
    {
        public MultiItemViewModel<ToolTypeViewModel, int> ToolsType { get; set; }
        public ToolsTypeFilterViewModel ToolsTypeFilters { get; set; }
    }
}