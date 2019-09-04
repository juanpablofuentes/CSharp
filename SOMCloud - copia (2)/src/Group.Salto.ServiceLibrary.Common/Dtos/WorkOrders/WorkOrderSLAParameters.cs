using System.Threading;

namespace Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders
{
    public class WorkOrderSLAParameters
    {
        public string ReferenceDate { get; set; }
        public Entities.Tenant.WorkOrderCategories Category { get; set; }
        public int Minutes { get; set; }
        public bool NaturalMinutes { get; set; }
        public WorkOrderEditDto WorkOrder { get; set; }
        public string InternalIdentifier { get; set; }
        public bool DoLog { get; set; } = false;
        public string SLAName { get; set; }
        public string CategoryName { get; set; }
        public CancellationToken Token { get; set; }
        public bool LogTime { get; set; } = false;
    }
}