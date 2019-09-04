using Group.Salto.ServiceLibrary.Common.Dtos.Contacts;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Contracts
{
    public class ContractDto
    {
        public int Id { get; set; }
        public string Object { get; set; }
        public string Reference { get; set; }
        public int ContractTypeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ContractUrl { get; set; }
        public string Signer { get; set; }
        public bool Active { get; set; }
        public int? ClientId { get; set; }
        public int? PeopleId { get; set; }
        public string Observations { get; set; }
        public IList<ContactsDto> ContactsSelected { get; set; }
    }
}