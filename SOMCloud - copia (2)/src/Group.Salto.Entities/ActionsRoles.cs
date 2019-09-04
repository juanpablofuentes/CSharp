namespace Group.Salto.Entities
{
    public class ActionsRoles
    {
        public string RoleId { get; set; }
        public int ActionId { get; set; }

        public Roles Roles { get; set; }
        public Actions Actions { get; set; }
    }
}
