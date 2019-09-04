using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class PointsRate : BaseEntity
    {
        public string ErpReference { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<ItemsPointsRate> ItemsPointsRate { get; set; }
        public ICollection<People> People { get; set; }
    }
}
