using Group.Salto.Common;
using System;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class AssetsAudit : BaseEntity
    {
        public int? AssetId { get; set; }
        public DateTime RegistryDate { get; set; }
        public string UserName { get; set; }

        public Assets Asset { get; set; }
        public ICollection<AssetsAuditChanges> AssetsAuditChanges { get; set; }
    }
}
