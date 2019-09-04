using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Modules
{
    public class ModuleBaseViewModel
    {
        public Guid? Id { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }
    }
}