using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class Brands : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }

        public ICollection<Models> Models { get; set; }
    }
}
