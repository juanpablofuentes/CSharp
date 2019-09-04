using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.UserConfigurationRolesTenant;

namespace Group.Salto.ServiceLibrary.Common.Contracts.UserConfigurationRolesTenant
{
    public interface IUserConfigurationRolesTenantService
    {
        string GetRoleIdByUserId(int userConfigurationId);
        ResultDto<UserConfigurationRolesTenantDto> CreateUserConfigurationRolTenant(UserConfigurationRolesTenantDto userConfigurationRolesTenant);
        ResultDto<bool> DeleteUserConfigurationRolTenant(int userConfigurationId);
    }
}