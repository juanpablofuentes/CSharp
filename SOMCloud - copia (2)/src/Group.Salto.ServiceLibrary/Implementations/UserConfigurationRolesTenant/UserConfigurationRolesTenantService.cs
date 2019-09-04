using Group.Salto.Common;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.UserConfigurationRolesTenant;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.UserConfigurationRolesTenant;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Implementations.UserConfigurationRolesTenant
{
    public class UserConfigurationRolesTenantService : BaseService, IUserConfigurationRolesTenantService
    {
        private IUserConfigurationRolesTenantRepository _userConfigurationRolesTenantRepository;

        public UserConfigurationRolesTenantService(ILoggingService logginingService,
            IUserConfigurationRolesTenantRepository userConfigurationRolesTenantRepository) : base(logginingService)
        {
            _userConfigurationRolesTenantRepository = userConfigurationRolesTenantRepository ?? throw new ArgumentNullException(nameof(IUserConfigurationRolesTenantRepository));
        }

        public string GetRoleIdByUserId(int userConfigurationId)
        {
            return _userConfigurationRolesTenantRepository.GetRoleIdByUserId(userConfigurationId);
        }

        public ResultDto<UserConfigurationRolesTenantDto> CreateUserConfigurationRolTenant(UserConfigurationRolesTenantDto model)
        {
            LogginingService.LogInfo($"Create - UserConfigurationRolTenant");
            var resultSave = _userConfigurationRolesTenantRepository.CreateUserConfigurationRolTenant(model.ToEntity());
            return ProcessResult(resultSave.Entity.ToDto(), resultSave); 
        }

        public ResultDto<bool> DeleteUserConfigurationRolTenant(int userConfigurationId)
        {
            LogginingService.LogInfo($"Delete UserConfigurationRolTenant by userConfigurationId {userConfigurationId}");
            ResultDto<bool> result = null;
            var localModel = _userConfigurationRolesTenantRepository.GetByUserConfigurationId(userConfigurationId);
            if (localModel != null)
            {
                var resultSave = _userConfigurationRolesTenantRepository.DeleteUserConfigurationRolTenant(localModel);
                result = ProcessResult(resultSave.IsOk, resultSave);
            }
            return result ?? new ResultDto<bool>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = false,
            };
        }
    }
}