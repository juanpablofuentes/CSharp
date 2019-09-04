namespace Group.Salto.Entities.Tenant
{
    public class LocationsFinalClients
    {
        public int FinalClientId { get; set; }
        public int LocationId { get; set; }
        public int? PeopleCommercialId { get; set; }
        public int OriginId { get; set; }
        public string CompositeCode { get; set; }

        public FinalClients FinalClient { get; set; }
        public Locations Location { get; set; }
        public People PeopleCommercial { get; set; }
    }
}
