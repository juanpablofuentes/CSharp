using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities
{
    public class Countries : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Regions> Regions { get; set; }
    }
}
