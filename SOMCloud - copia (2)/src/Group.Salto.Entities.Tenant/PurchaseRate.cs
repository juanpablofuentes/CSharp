using Group.Salto.Common;
using Group.Salto.Common.Entities;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class PurchaseRate : SoftDeleteBaseEntity
    {
        public string ErpReference { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<ItemsPurchaseRate> ItemsPurchaseRate { get; set; }
        public ICollection<SubContracts> SubContracts { get; set; }

    }
}
