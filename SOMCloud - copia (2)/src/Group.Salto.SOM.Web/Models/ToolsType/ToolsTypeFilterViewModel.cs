using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.ToolsType
{
    public class ToolsTypeFilterViewModel: BaseFilter
    {
        public ToolsTypeFilterViewModel()
        {
            OrderBy = "Name";
        }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}