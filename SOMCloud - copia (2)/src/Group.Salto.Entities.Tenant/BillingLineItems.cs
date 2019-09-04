using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class BillingLineItems : BaseEntity
    {
        public int ProjectId { get; set; }
        public int WorkOrderCategoryId { get; set; }
        public int? TaskId { get; set; }
        public int? PredefinedServiceId { get; set; }
        public string Type { get; set; }

        public PredefinedServices PredefinedService { get; set; }
        public Projects Project { get; set; }
        public Tasks Task { get; set; }
        public WorkOrderCategories WorkOrderCategory { get; set; }
        public ICollection<BillingItems> BillingItems { get; set; }
    }
}
