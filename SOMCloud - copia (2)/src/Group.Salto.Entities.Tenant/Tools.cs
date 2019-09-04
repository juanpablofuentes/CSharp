using Group.Salto.Common;
using Group.Salto.Common.Entities;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class Tools : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Observations { get; set; }
        public int? VehicleId { get; set; }
        public Vehicles Vehicle { get; set; }
        public ICollection<ToolsToolTypes> ToolsToolTypes { get; set; }
    }
}
