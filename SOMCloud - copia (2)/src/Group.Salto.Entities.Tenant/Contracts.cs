using Group.Salto.Common.Entities;
using System;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class Contracts : SoftDeleteBaseEntity
    {
        public string Object { get; set; }
        public string Signer { get; set; }
        public string Observations { get; set; }
        public string Reference { get; set; }
        public bool Active { get; set; }
        public int ContractTypeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ContractUrl { get; set; }
        public int? PeopleId { get; set; }
        public int? ClientId { get; set; }

        public Clients Client { get; set; }
        public People People { get; set; }
        public ICollection<ContractContacts> ContractContacts { get; set; }
        public ICollection<Projects> Projects { get; set; }
        public ICollection<AssetsContracts> AssetContracts { get; set; }
    }
}