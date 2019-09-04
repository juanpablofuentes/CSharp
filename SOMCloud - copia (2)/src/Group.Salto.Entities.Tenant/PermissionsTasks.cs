namespace Group.Salto.Entities.Tenant
{
    public class PermissionsTasks
    {
        public int PermissionId { get; set; }
        public int TaskId { get; set; }

        public Permissions Permission { get; set; }
        public Tasks Task { get; set; }
    }
}
