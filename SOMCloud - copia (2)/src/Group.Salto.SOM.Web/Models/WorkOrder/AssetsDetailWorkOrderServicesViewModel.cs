using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.WorkOrder
{
    public class AssetsDetailWorkOrderServicesViewModel : FormState
    {
        public AssetsDetailWorkOrderServicesViewModel()
        {
            Service = new List<AssetsDetailServicesViewModel>();
        }

        public int Id { get; set; }
        public string Observations { get; set; }
        public string Repair { get; set; }
        public List<AssetsDetailServicesViewModel> Service { get; set; }
    }
}