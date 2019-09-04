using Group.Salto.Common.Entities;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class SymptomCollections : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public string Element { get; set; }
        public string Description { get; set; }

        public ICollection<SymptomCollectionSymptoms> SymptomCollectionSymptoms { get; set; }
    }
}