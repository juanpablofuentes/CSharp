namespace Group.Salto.Entities.Tenant
{
    public class PredefinedServicesPermission
    {
        public int PredefinedServiceId { get; set; }
        public int PermissionId { get; set; }

        public PredefinedServices PredefinedService { get; set; }
        public Permissions Permission { get; set; }
    }
}
