using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class AssetStatuses : BaseEntity
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public bool? IsDefault { get; set; }
        public bool? IsRetiredState { get; set; }

        public ICollection<Assets> Assets { get; set; }
    }
}