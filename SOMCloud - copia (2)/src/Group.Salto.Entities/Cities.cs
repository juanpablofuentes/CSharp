using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities
{
    public class Cities : BaseEntity
    {
        public string Name { get; set; }
        public int MunicipalityId { get; set; }

        public Municipalities Municipality { get; set; }
        public ICollection<PostalCodes> PostalCodes { get; set; }
        public ICollection<CitiesOtherNames> CitiesOtherNames { get; set; }
    }
}
