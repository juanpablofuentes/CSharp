using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.AssetsAudit
{
    public class AssetsChangesViewModel
    {
        public string Property { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}