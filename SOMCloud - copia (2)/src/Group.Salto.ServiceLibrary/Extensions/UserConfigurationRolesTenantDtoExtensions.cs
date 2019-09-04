using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.UserConfigurationRolesTenant;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class UserConfigurationRolesTenantDtoExtensions
    {
        public static UserConfigurationRolesTenant ToEntity(this UserConfigurationRolesTenantDto source)
        {
            UserConfigurationRolesTenant result = null;
            if (source != null)
            {
                result = new UserConfigurationRolesTenant()
                {
                    UserConfigurationId = source.UserConfigurationId,
                    RolesTenantId = source.RolesTenantId
                };
            }
            return result;
        }

        public static UserConfigurationRolesTenantDto ToDto(this UserConfigurationRolesTenant source)
        {
            UserConfigurationRolesTenantDto result = null;
            if (source != null)
            {
                result = new UserConfigurationRolesTenantDto()
                {
                    UserConfigurationId = source.UserConfigurationId,
                    RolesTenantId = source.RolesTenantId
                };
            }
            return result;
        }
    }
}