using Group.Salto.Common;
using System;

namespace Group.Salto.Entities.Tenant
{
    public class PlanificationProcessWorkOrderChangeTracker : BaseEntity
    {
        public int PlanificationProcessId { get; set; }
        public DateTime? LastCheckTime { get; set; }
        public int? WorkOrderId { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
