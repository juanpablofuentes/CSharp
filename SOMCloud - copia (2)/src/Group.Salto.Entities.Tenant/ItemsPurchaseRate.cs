using Group.Salto.Common;

namespace Group.Salto.Entities.Tenant
{
    public class ItemsPurchaseRate : BaseEntity
    {
        public int ItemId { get; set; }
        public int PurchaseRateId { get; set; }
        public double Price { get; set; }

        public Items Item { get; set; }
        public PurchaseRate PurchaseRate { get; set; }
    }
}
