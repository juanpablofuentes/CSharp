using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Assets
{
    public class SecondaryDetailViewModel
    {
        public SecondaryDetailViewModel()
        {
            Warranty = new GuaranteeViewModel();
            HiredServices = new List<HiredServicesViewModel>();
        }

        public GuaranteeViewModel Warranty { get; set; }        
        public List<HiredServicesViewModel> HiredServices { get; set; }
    }
}