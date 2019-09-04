using Group.Salto.Common;
using System;

namespace Group.Salto.Entities
{
    public class DamagedEquipment : BaseEntity<Guid>
    {
        public string Name { get; set; }
    }
}