using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class Items : BaseEntity
    {
        public string ErpReference { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool SyncErp { get; set; }
        public int Type { get; set; }
        public byte[] ErpVersion { get; set; }
        public int? SubFamiliesId { get; set; }
        public string InternalReference { get; set; }
        public bool TrackBySerialNumber { get; set; }
        public bool InDepot { get; set; }
        public bool IsBlocked { get; set; }
        public byte[] Picture { get; set; }

        public ICollection<BillLine> BillLine { get; set; }
        public ICollection<BillingRuleItem> BillingRuleItem { get; set; }
        public ICollection<ItemsPointsRate> ItemsPointsRate { get; set; }
        public ICollection<ItemsPurchaseRate> ItemsPurchaseRate { get; set; }
        public ICollection<ItemsSalesRate> ItemsSalesRate { get; set; }
        public ICollection<ItemsSerialNumber> ItemsSerialNumber { get; set; }
        public ICollection<WarehouseMovements> WarehouseMovements { get; set; }
        public ICollection<DnAndMaterialsAnalysis> DnAndMaterialsAnalysis { get; set; }
        public SubFamilies SubFamilies { get; set; }
    }
}