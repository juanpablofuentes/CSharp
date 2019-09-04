using Group.Salto.Common;

namespace Group.Salto.Entities.Tenant
{
    public partial class BillingRuleItem : BaseEntity
    {
        public int BillingRuleId { get; set; }
        public string Units { get; set; }
        public int ItemId { get; set; }

        public BillingRule BillingRule { get; set; }
        public Items Item { get; set; }
    }
}
