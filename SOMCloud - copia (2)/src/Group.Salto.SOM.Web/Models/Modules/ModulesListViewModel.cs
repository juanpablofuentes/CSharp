using Group.Salto.Controls.Table.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Modules
{
    public class ModulesListViewModel
    {
        public MultiItemViewModel<ModuleBaseViewModel, Guid> ModuleList { get; set; }
        public ModuleFilterViewModel ModuleFilter { get; set; }
    }
}