using Group.Salto.Common;
using Group.Salto.Common.Entities;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class SalesRate : SoftDeleteBaseEntity
    {
        public string ErpReference { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<ItemsSalesRate> ItemsSalesRate { get; set; }
    }
}
