namespace Group.Salto.Entities.Tenant
{
    public class ProjectsPermissions
    {
        public int PermissionId { get; set; }
        public int ProjectId { get; set; }
        public Projects Project { get; set; }
        public Permissions Permission { get; set; }
    }
}
