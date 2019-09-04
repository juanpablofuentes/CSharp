using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities
{
    public class States : BaseEntity
    {
        public string Name { get; set; }
        public int RegionId { get; set; }

        public Regions Region { get; set; }
        public ICollection<Municipalities> Municipalities { get; set; }
        public ICollection<StatesOtherNames> StatesOtherNames { get; set; }
    }
}
