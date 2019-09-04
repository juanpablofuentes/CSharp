using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class Zones : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<PreconditionsLiteralValues> PreconditionsLiteralValues { get; set; }
        public ICollection<ZoneProject> ZoneProject { get; set; }
    }
}
