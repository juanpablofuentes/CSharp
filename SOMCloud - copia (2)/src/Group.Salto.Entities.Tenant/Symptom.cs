using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class Symptom : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? SymptomFatherId { get; set; }

        public Symptom SymptomFather { get; set; }
        public ICollection<Symptom> InverseSymptomFather { get; set; }
        public ICollection<SymptomCollectionSymptoms> SymptomCollectionSymptoms { get; set; }
    }
}