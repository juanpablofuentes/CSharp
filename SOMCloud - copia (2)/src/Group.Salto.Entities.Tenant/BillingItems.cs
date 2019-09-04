using Group.Salto.Common;

namespace Group.Salto.Entities.Tenant
{
    public class BillingItems : BaseEntity
    {
        public int BillingLineItemId { get; set; }
        public string Reference { get; set; }
        public int Units { get; set; }

        public BillingLineItems BillingLineItem { get; set; }
    }
}
