using Group.Salto.ServiceLibrary.Common.Dtos.Contacts;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.FinalClients
{
    public class FinalClientsDetailDto : FinalClientsDto
    {
        public string IdExtern { get; set; }
        public int OriginId { get; set; }
        public string Description { get; set; }
        public string Nif { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }
        public string Fax { get; set; }
        public string Status { get; set; }
        public string Observations { get; set; }
        public int? PeopleCommercialId { get; set; }
        public string Code { get; set; }
        public IList<ContactsDto> Contacts { get; set; }
    }
}