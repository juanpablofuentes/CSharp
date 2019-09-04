using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public  class HiredServices : BaseEntity
    {
        public string Name { get; set; }
        public string Observations { get; set; }

        public ICollection<AssetsHiredServices> AssetsHiredServices { get; set; }
    }
}
