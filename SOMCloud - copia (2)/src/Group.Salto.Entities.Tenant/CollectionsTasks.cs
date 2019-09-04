namespace Group.Salto.Entities.Tenant
{
    public class CollectionsTasks
    {
        public int CollectionsExtraFieldId { get; set; }
        public int ExtraFieldId { get; set; }
        public int? Position { get; set; }

        public CollectionsExtraField CollectionsExtraField { get; set; }
        public ExtraFields ExtraField { get; set; }
    }
}
