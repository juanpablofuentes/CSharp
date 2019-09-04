using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.AssetsAudit
{
    public class AssetsAuditViewModel
    {
        public DateTime Registry { get; set; }        
        public string  UserName { get; set; }
        public IList<AssetsChangesViewModel> Changes { get; set; }
    }
}