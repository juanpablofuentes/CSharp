using Group.Salto.Common;
using System;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant 
{
    public class TasksTypes : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Tasks> Tasks { get; set; }
    }
}