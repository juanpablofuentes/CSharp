using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class PredefinedServices : BaseEntity
    {
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public bool LinkClosingCode { get; set; }
        public int? CollectionExtraFieldId { get; set; }
        public bool? Billable { get; set; }
        public bool? MustValidate { get; set; }

        public CollectionsExtraField CollectionExtraField { get; set; }
        public Projects Project { get; set; }
        public ICollection<BillingLineItems> BillingLineItems { get; set; }
        public ICollection<DerivedServices> DerivedServices { get; set; }
        public ICollection<PredefinedServicesPermission> PredefinedServicesPermission { get; set; }
        public ICollection<Services> Services { get; set; }
        public ICollection<Tasks> Tasks { get; set; }
    }
}
