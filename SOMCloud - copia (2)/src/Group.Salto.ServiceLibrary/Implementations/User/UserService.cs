using Group.Salto.Common;
using Group.Salto.Common.Constants.People;
using Group.Salto.Entities;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.User;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.People;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.ServiceLibrary.Common.Contracts.Idantity;

namespace Group.Salto.ServiceLibrary.Implementations.User
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IIdentityService _identityService;

        public UserService(ILoggingService logginingService, 
                           IUserRepository userRepository,
                           IIdentityService identityService) : base(logginingService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(IUserRepository));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(IIdentityService));
        }

        public ResultDto<IList<PeopleListDto>> GetAllFilteredByTenant(PeopleFilterDto filter, Guid tenantId)
        {
            LogginingService.LogInfo($"Get Peoples filtered");

            IQueryable<Users> query = _identityService.GetAllByCusomterNotDeleted(tenantId);
            query = query.WhereIfNotDefault(filter.Name, au => au.UserName.Contains(filter.Name));

            return ProcessResult(query.ToList().ToListDto());
        }

        public ResultDto<bool> DeleteUser(int userConfigurationId)
        {
            LogginingService.LogInfo($"Delete User");
            List<ErrorDto> errors = new List<ErrorDto>();
            bool deleteResult = false;

            Users userToDelete = _identityService.GetByUserConfigurationId(userConfigurationId);
            if (userToDelete != null)
            {
                deleteResult = _userRepository.DeleteUser(userToDelete);
            }
            else
            {
                errors.Add(new ErrorDto() { ErrorType = ErrorType.EntityAlredyExists, ErrorMessageKey = PeopleConstants.PeopleNotExist });
            }

            return ProcessResult(deleteResult, errors);
        }
    }
}