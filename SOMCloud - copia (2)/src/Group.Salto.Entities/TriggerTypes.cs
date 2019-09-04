using Group.Salto.Common;
using System;

namespace Group.Salto.Entities
{
    public class TriggerTypes : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}