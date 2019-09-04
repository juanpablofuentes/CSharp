using Group.Salto.Common;
using System;

namespace Group.Salto.Entities.Tenant
{
    public class SgsClosingInfo : BaseEntity
    {
        public int WorkOrderId { get; set; }
        public string ParametersSent { get; set; }
        public string Response { get; set; }
        public int StatusResponse { get; set; }
        public DateTime SentDate { get; set; }

        public WorkOrders WorkOrder { get; set; }
    }
}
