using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class Warehouses : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string ErpReference { get; set; }

        public ICollection<People> People { get; set; }
        public ICollection<WarehouseMovementEndpoints> WarehouseMovementEndpoints { get; set; }
    }
}