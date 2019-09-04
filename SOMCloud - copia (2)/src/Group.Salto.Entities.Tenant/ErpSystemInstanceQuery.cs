using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class ErpSystemInstanceQuery : BaseEntity
    {
        public int ErpSystemInstanceId { get; set; }
        public string Name { get; set; }
        public string SqlQuery { get; set; }

        public ErpSystemInstance ErpSystemInstance { get; set; }
        public ICollection<ExtraFields> ExtraFields { get; set; }
    }
}
