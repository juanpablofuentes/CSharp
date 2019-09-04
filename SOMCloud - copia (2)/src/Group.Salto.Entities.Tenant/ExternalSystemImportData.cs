using System;

namespace Group.Salto.Entities.Tenant
{
    public class ExternalSystemImportData
    {
        public string ImportCode { get; set; }
        public string ExternalSystem { get; set; }
        public string Property { get; set; }
        public string Value { get; set; }
        public int? WorkOrderId { get; set; }
        public DateTime CreationDate { get; set; }

        public WorkOrders WorkOrder { get; set; }
    }
}
