using Group.Salto.Common;
using System;
using System.Collections.Generic;

namespace Group.Salto.Entities
{
    public class ActionGroups : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<ModuleActionGroups> ModuleActionGroups { get; set; }
        public ICollection<RolesActionGroupsActions> RolesActionGroupsActions { get; set; }
    }
}