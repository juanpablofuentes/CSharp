using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class Usages : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Assets> Assets { get; set; }
    }
}
