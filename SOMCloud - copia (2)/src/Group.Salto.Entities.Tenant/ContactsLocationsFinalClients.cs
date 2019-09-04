namespace Group.Salto.Entities.Tenant
{
    public class ContactsLocationsFinalClients
    {
        public int LocationId { get; set; }
        public int ContactId { get; set; }

        public Contacts Contact { get; set; }
        public Locations Location { get; set; }
    }
}
