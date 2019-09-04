using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities
{
    public class Regions : BaseEntity
    {
        public string Name { get; set; }
        public int CountryId { get; set; }

        public Countries Country { get; set; }
        public ICollection<States> States { get; set; }
    }
}
