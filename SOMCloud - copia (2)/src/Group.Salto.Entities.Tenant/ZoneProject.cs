using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class ZoneProject : BaseEntity
    {
        public int ZoneId { get; set; }
        public int? ProjectId { get; set; }

        public Projects Project { get; set; }
        public Zones Zone { get; set; }
        public ICollection<ZoneProjectPostalCode> ZoneProjectPostalCode { get; set; }
    }
}
