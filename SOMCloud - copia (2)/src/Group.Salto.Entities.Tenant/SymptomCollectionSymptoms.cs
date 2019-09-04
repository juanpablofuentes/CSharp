namespace Group.Salto.Entities.Tenant
{
    public class SymptomCollectionSymptoms
    {
        public int SymptomId { get; set; }
        public int SymptomCollectionId { get; set; }

        public Symptom Symptom { get; set; }
        public SymptomCollections SymptomCollection { get; set; }
    }
}