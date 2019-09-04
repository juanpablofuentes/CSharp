using Group.Salto.Common;
using System.Collections.Generic;
using Group.Salto.Common.Entities;

namespace Group.Salto.Entities.Tenant
{
    public class SubContracts : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Observations { get; set; }
        public int Priority { get; set; }
        public int? PurchaseRateId { get; set; }
        public PurchaseRate PurchaseRate { get; set; }
        public ICollection<DerivedServices> DerivedServices { get; set; }
        public ICollection<KnowledgeSubContracts> KnowledgeSubContracts { get; set; }
        public ICollection<People> People { get; set; }
        public ICollection<Services> Services { get; set; }
    }
}