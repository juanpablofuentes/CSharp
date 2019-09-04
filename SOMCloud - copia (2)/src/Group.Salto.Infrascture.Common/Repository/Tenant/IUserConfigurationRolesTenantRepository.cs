using Group.Salto.Common;
using Group.Salto.Entities.Tenant;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IUserConfigurationRolesTenantRepository
    {
        string GetRoleIdByUserId(int userConfigurationId);
        UserConfigurationRolesTenant GetByUserConfigurationId(int userConfigurationId);
        SaveResult<UserConfigurationRolesTenant> CreateUserConfigurationRolTenant(UserConfigurationRolesTenant userConfigurationRolesTenant);
        SaveResult<UserConfigurationRolesTenant> DeleteUserConfigurationRolTenant(UserConfigurationRolesTenant entity);
    }
}