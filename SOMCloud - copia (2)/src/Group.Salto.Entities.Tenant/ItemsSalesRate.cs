using Group.Salto.Common;

namespace Group.Salto.Entities.Tenant
{
    public class ItemsSalesRate : BaseEntity
    {
        public int ItemId { get; set; }
        public int SalesRateId { get; set; }
        public double Price { get; set; }

        public Items Item { get; set; }
        public SalesRate SalesRate { get; set; }
    }
}
