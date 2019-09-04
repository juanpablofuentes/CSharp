using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class BillingRule : BaseEntity
    {
        public int TaskId { get; set; }
        public string Condition { get; set; }
        public int ErpSystemInstanceId { get; set; }

        public ErpSystemInstance ErpSystemInstance { get; set; }
        public Tasks Task { get; set; }
        public ICollection<BillingRuleItem> BillingRuleItem { get; set; }
    }
}
