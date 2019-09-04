using Group.Salto.SOM.Web.Models.ZoneProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.ZoneProjectPostalCode
{
    public class ZoneProjectPostalCodeViewModel
    {
        public int PostalCodeId { get; set; }
        public int ZoneProjectId { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        public ZoneProjectViewModel ZoneProject{ get; set; }
    }
}
