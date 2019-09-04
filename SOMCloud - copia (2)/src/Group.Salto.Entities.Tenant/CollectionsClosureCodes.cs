using Group.Salto.Common;
using System.Collections.Generic;
using Group.Salto.Common.Entities;

namespace Group.Salto.Entities.Tenant
{
    public class CollectionsClosureCodes : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<ClosingCodes> ClosingCodes { get; set; }
        public ICollection<Projects> Projects { get; set; }
    }
}
