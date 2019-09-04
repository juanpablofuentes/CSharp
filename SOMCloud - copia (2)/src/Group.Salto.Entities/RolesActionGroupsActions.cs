using System;

namespace Group.Salto.Entities
{
    public class RolesActionGroupsActions
    {
        public string RolId { get; set; }
        public Guid ActionGroupId { get; set; }
        public int ActionId { get; set; }

        public Roles Roles { get; set; }
        public ActionGroups ActionGroups { get; set; }
        public Actions Actions { get; set; }
    }
}