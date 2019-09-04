using Group.Salto.Common;
using System;

namespace Group.Salto.Entities
{
    public class WarehouseMovementTypes : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public bool IsOutgoing { get; set; }
    }
}