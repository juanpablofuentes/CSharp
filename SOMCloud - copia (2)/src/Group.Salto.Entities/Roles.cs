using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Group.Salto.Entities
{
    public class Roles : IdentityRole
    {
        public string Description { get; set; }
        //TODO: Carmen. RolesActionGroupsActions
        public ICollection<ActionsRoles> ActionsRoles { get; set; }
        public ICollection<RolesActionGroupsActions> RolesActionGroupsActions { get; set; }
    }
}