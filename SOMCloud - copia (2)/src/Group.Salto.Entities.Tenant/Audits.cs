using Group.Salto.Common;
using System;

namespace Group.Salto.Entities.Tenant
{
    public class Audits : BaseEntity
    {
        public string Name { get; set; }
        public int WorkOrderId { get; set; }
        public DateTime DataHora { get; set; }
        public int UserConfigurationId { get; set; }
        public string Origin { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? Height { get; set; }
        public string Observations { get; set; }
        public int? UserConfigurationSupplanterId { get; set; }
        public int? TaskId { get; set; }

        public Tasks Task { get; set; }
        public UserConfiguration UserConfiguration { get; set; }
        public UserConfiguration UserConfigurationSupplanter { get; set; }
        public WorkOrders WorkOrder { get; set; }
    }
}
