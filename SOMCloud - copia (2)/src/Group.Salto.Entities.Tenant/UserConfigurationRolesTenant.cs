namespace Group.Salto.Entities.Tenant
{
    public class UserConfigurationRolesTenant
    {
        public int UserConfigurationId { get; set; }
        public string RolesTenantId { get; set; }

        public UserConfiguration UserConfiguration { get; set; }
        public RolesTenant RolesTenant { get; set; }
    }
}