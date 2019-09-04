using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.ServiceGauges
{
    public class ServiceGaugesListViewModel
    {
        public ServiceGaugesFilterViewModel Filter { get; set; }
        public ServiceGaugesResultFilterViewModel Result { get; set; }
        public ServiceGaugesProjectReporViewModel Economic { get; set; }
    }
}