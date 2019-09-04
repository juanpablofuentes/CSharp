using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class Families : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<SubFamilies> SubFamilies { get; set; }
    }
}
