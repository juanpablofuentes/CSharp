using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities
{
    public class Municipalities : BaseEntity
    {
        public string Name { get; set; }
        public int? MunicipalityCode { get; set; }
        public int StateId { get; set; }

        public States State { get; set; }
        public ICollection<Cities> Cities { get; set; }
    }
}
