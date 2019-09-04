using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities
{
    public class PredefinedDayStates : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<PredefinedReasons> PredefinedReasons { get; set; }
    }
}
