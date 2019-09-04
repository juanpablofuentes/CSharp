using Group.Salto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.Entities
{
    public class ReferenceTimeSla: BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        
    }
}
