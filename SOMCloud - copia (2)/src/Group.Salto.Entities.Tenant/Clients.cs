using Group.Salto.Common.Entities;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class Clients : SoftDeleteBaseEntity
    {
        public string CorporateName { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Observations { get; set; }
        public int? IdIcg { get; set; }
        public string InternCode { get; set; }
        public string ContableCode { get; set; }
        public string ComercialName { get; set; }
        public string Alias { get; set; }
        public string Address { get; set; }
        public int? MunicipalityId { get; set; }
        public int? PostalCodeId { get; set; }
        public string MobilePhone { get; set; }
        public string Mail { get; set; }
        public string Web { get; set; }
        public string BankCode { get; set; }
        public string BranchNumber { get; set; }
        public string ControlDigit { get; set; }
        public string AccountNumber { get; set; }
        public string SwiftCode { get; set; }
        public string BankName { get; set; }
        public string BankAddress { get; set; }
        public string BankPostalCode { get; set; }
        public string BankCity { get; set; }
        public bool UnListed { get; set; }

        public ICollection<Contracts> Contracts { get; set; }
    }
}
