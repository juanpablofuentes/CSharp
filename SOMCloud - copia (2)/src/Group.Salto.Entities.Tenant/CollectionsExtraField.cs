using Group.Salto.Common;
using Group.Salto.Common.Entities;
using Group.Salto.Common.Entities.Contracts;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class CollectionsExtraField : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Observations { get; set; }

        public ICollection<CollectionsExtraFieldExtraField> CollectionsExtraFieldExtraField { get; set; }
        public ICollection<PredefinedServices> PredefinedServices { get; set; }
        public ICollection<Projects> Projects { get; set; }
    }
}
