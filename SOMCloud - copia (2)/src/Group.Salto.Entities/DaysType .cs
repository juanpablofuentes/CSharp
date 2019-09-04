using Group.Salto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.Entities
{
    public class DaysType : BaseEntity<Guid>
    {
        public string Name { get; set; }
    }
}