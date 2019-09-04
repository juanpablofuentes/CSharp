namespace Group.Salto.Entities.Tenant
{
    public class ContractContacts
    {
        public int ContractId { get; set; }
        public int ContactId { get; set; }

        public Contacts Contact { get; set; }
        public Contracts Contract { get; set; }
    }
}
