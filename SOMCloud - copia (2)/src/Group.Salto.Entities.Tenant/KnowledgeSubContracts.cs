namespace Group.Salto.Entities.Tenant
{
    public class KnowledgeSubContracts
    {
        public int KnowledgeId { get; set; }
        public int SubContractId { get; set; }
        public int Maturity { get; set; }

        public Knowledge Knowledge { get; set; }
        public SubContracts SubContract { get; set; }
    }
}
