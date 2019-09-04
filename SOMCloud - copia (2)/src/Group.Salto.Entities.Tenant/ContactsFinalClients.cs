namespace Group.Salto.Entities.Tenant
{
    public class ContactsFinalClients
    {
        public int FinalClientId { get; set; }
        public int ContactId { get; set; }

        public Contacts Contact { get; set; }
        public FinalClients FinalClient { get; set; }
    }
}
