using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class WarehouseMovementEndpoints : BaseEntity
    {
        public string Name { get; set; }
        public int? WarehouseId { get; set; }
        public int? AssetId { get; set; }

        public Warehouses Warehouse { get; set; }
        public Assets Asset { get; set; }
        public ICollection<WarehouseMovements> WarehouseMovementsFrom { get; set; }
        public ICollection<WarehouseMovements> WarehouseMovementsTo { get; set; }
    }
}