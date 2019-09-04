using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.People;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.User;
using System;
using Group.Salto.ServiceLibrary.Extensions;
using System.Collections.Generic;
using Group.Salto.Common;

namespace Group.Salto.ServiceLibrary.Implementations.People
{
    public class UserConfigurationService : BaseService, IUserConfigurationService
    {
        private readonly IUserConfigurationRepository _userConfigurationRepository;

        public UserConfigurationService(ILoggingService logginingService,
                                        IUserConfigurationRepository userConfigurationRepository)
            : base(logginingService)
        {
            _userConfigurationRepository = userConfigurationRepository ?? throw new ArgumentNullException($"{nameof(userConfigurationRepository)} is null ");
        }

        public ResultDto<UserConfigurationsDto> CreateUserConfiguration()
        {
            Guid newGuidId = Guid.NewGuid();
            UserConfigurationsDto userConfiguration = new UserConfigurationsDto() { GuidId = newGuidId };

            var newUserConfiguration = userConfiguration.ToEntity();
            var result = _userConfigurationRepository.CreateUserConfiguration(newUserConfiguration);

            return ProcessResult(result.Entity?.ToDto(), result);
        }

        public ResultDto<bool> DeleteUserConfiguration(int Id)
        {
            LogginingService.LogInfo($"Delete People");
            var UserConfigurationToDelete = _userConfigurationRepository.Find(x => x.Id == Id);
            ResultDto<bool> result = null;
            bool deleteResult = false;

            if (UserConfigurationToDelete != null)
            {
                deleteResult = _userConfigurationRepository.DeleteUserConfiguration(UserConfigurationToDelete);
            }

            return result ?? new ResultDto<bool>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = deleteResult,
            };
        }

    }
}