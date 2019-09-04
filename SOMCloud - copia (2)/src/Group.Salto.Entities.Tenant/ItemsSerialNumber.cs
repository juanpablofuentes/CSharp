using Group.Salto.Entities.Tenant.AttributedEntities;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class ItemsSerialNumber
    {
        public int ItemId { get; set; }
        public string SerialNumber { get; set; }
        public string Observations { get; set; }
        public int? ItemsSerialNumberStatusesId { get; set; }

        public Items Item { get; set; }
        public ICollection<BillLine> BillLine { get; set; }
        public ItemsSerialNumberStatuses ItemsSerialNumberStatuses { get; set; }
        public ICollection<ItemsSerialNumberAttributeValues> ItemsSerialNumberAttributeValues { get; set; }
        public ICollection<WarehouseMovements> WarehouseMovements { get; set; }
    }
}
