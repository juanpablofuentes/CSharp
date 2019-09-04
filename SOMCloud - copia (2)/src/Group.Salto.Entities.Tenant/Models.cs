using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class Models : BaseEntity
    {
        public int BrandId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }

        public Brands Brand { get; set; }
        public ICollection<Assets> Assets { get; set; }
    }
}
