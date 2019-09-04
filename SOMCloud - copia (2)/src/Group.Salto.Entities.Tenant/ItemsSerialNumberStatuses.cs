using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class ItemsSerialNumberStatuses : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<ItemsSerialNumber> ItemsSerialNumber { get; set; }
    }
}