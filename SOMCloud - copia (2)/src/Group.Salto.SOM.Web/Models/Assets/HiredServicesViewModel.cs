using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Assets
{
    public class HiredServicesViewModel
    {
        public int HiredServicesId { get; set; }
        [MaxLength(50)]
        public string HiredServicesName { get; set; }
        [MaxLength(1000)]
        public string HiredServicesObservations { get; set; }
    }
}