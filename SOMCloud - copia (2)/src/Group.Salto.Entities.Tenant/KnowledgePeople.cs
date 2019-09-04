namespace Group.Salto.Entities.Tenant
{
    public class KnowledgePeople
    {
        public int KnowledgeId { get; set; }
        public int PeopleId { get; set; }
        public int Maturity { get; set; }

        public Knowledge Knowledge { get; set; }
        public People People { get; set; }
    }
}
