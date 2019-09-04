using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders
{
    public class AssetsDetailWorkOrderServicesDto
    {
        public AssetsDetailWorkOrderServicesDto()
        {
            Services = new List<AssetsDetailServicesDto>();
        }

        public int Id { get; set; }
        public string Observations { get; set; }
        public string Repair { get; set; }
        public List<AssetsDetailServicesDto> Services { get; set; }
    }
}