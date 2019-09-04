using Group.Salto.Common;
using System;
using System.Collections.Generic;
using Group.Salto.Common.Entities;

namespace Group.Salto.Entities.Tenant
{
    public class RepetitionParameters : BaseEntity<Guid>
    {
        public int Days { get; set; }
        public Guid IdDaysType { get; set; }
        public Guid IdCalculationType { get; set; }
        public Guid IdDamagedEquipment { get; set; }
    }
}