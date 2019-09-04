using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities
{
    public class Actions : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        //TODO: Carmen. RolesActionGroupsActions
        public ICollection<ActionsRoles> ActionsRoles { get; set; }
        public ICollection<RolesActionGroupsActions> RolesActionGroupsActions { get; set; }
    }
}
