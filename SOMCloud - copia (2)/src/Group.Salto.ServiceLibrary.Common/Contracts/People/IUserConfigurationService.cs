using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.User;

namespace Group.Salto.ServiceLibrary.Common.Contracts.People
{
    public interface IUserConfigurationService
    {
        ResultDto<UserConfigurationsDto> CreateUserConfiguration();
        ResultDto<bool> DeleteUserConfiguration(int Id);
    }
}