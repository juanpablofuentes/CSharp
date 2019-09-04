using Group.Salto.Common;
using Group.Salto.Entities;
using System;
using System.Collections.Generic;

namespace Group.Salto.Entities
{
    public class CalculationType : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public int NumDays { get; set; }
    }
}