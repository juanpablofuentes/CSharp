using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class Contacts : BaseEntity
    {
        public string Name { get; set; }
        public string FirstSurname { get; set; }
        public string SecondSurname { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Position { get; set; }

        public ICollection<ContactsFinalClients> ContactsFinalClients { get; set; }
        public ICollection<ContactsLocationsFinalClients> ContactsLocationsFinalClients { get; set; }
        public ICollection<ContractContacts> ContractContacts { get; set; }
        public ICollection<ProjectsContacts> ProjectsContacts { get; set; }
    }
}
