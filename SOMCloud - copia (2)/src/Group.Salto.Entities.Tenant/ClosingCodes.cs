using Group.Salto.Common;
using System.Collections.Generic;
using Group.Salto.Common.Entities;

namespace Group.Salto.Entities.Tenant
{
    public class ClosingCodes : SoftDeleteBaseEntity
    {
        public int? CollectionsClosureCodesId { get; set; }
        public int? ClosingCodesFatherId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ClosingCodes ClosingCodesFather { get; set; }
        public CollectionsClosureCodes CollectionsClosureCodes { get; set; }
        public ICollection<DerivedServices> DerivedServicesClosingCodesIdN1Navigation { get; set; }
        public ICollection<DerivedServices> DerivedServicesClosingCodesIdN2Navigation { get; set; }
        public ICollection<DerivedServices> DerivedServicesClosingCodesIdN3Navigation { get; set; }
        public ICollection<ClosingCodes> InverseClosingCodesFather { get; set; }
        public ICollection<Services> ServicesClosingCode { get; set; }
        public ICollection<Services> ServicesClosingCodeFirst { get; set; }
        public ICollection<Services> ServicesClosingCodeSecond { get; set; }
        public ICollection<Services> ServicesClosingCodeThird { get; set; }
    }
}
