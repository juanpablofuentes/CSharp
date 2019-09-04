using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class ErpSystemInstance : BaseEntity
    {
        public string ErpSystemIdentifier { get; set; }
        public string Name { get; set; }
        public string DatabaseIpAddress { get; set; }
        public string DatabaseUser { get; set; }
        public string DatabasePwd { get; set; }
        public string DatabaseName { get; set; }

        public WsErpSystemInstance WsErpSystemInstance { get; set; }
        public ICollection<Bill> Bill { get; set; }
        public ICollection<BillingRule> BillingRule { get; set; }
        public ICollection<ErpItemsSyncConfig> ErpItemsSyncConfig { get; set; }
        public ICollection<ErpSystemInstanceQuery> ErpSystemInstanceQuery { get; set; }
    }
}
