using Group.Salto.Common;
using Group.Salto.Common.Entities;

namespace Group.Salto.Entities
{
    public class Origins : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Observations { get; set; }
        public int CanBeDeleted { get; set; }
    }
}
