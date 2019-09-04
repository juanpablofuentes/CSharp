using Group.Salto.Common;
using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.People;
using Group.Salto.Entities;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Idantity;
using Group.Salto.ServiceLibrary.Common.Contracts.Rol;
using Group.Salto.ServiceLibrary.Common.Contracts.RolesTenant;
using Group.Salto.ServiceLibrary.Common.Contracts.User;
using Group.Salto.ServiceLibrary.Common.Contracts.UserConfigurationRolesTenant;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.People;
using Group.Salto.ServiceLibrary.Common.Dtos.User;
using Group.Salto.ServiceLibrary.Common.Dtos.UserConfigurationRolesTenant;
using Group.Salto.ServiceLibrary.Extensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.ServiceLibrary.Implementations.User
{
    public class AccessService : BaseService, IAccessService
    {
        private readonly IPasswordHasher<Users> _passwordHasher;
        private readonly IIdentityService _identityService;
        private readonly IUserService _userService;
        private readonly IUserConfigurationRolesTenantService _userConfigurationRolesTenantService;
        private readonly IRolTenantService _rolTenantService;

        public AccessService(ILoggingService logginingService,
                             IUserService userService,
                             IPasswordHasher<Users> passwordHasher,
                             IIdentityService identityService,
                             IUserConfigurationRolesTenantService userConfigurationRolesTenantService,
                             IRolTenantService rolTenantService) : base(logginingService)
        {
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException($"{nameof(passwordHasher)} is null ");
            _identityService = identityService ?? throw new ArgumentNullException($"{nameof(identityService)} is null ");
            _userService = userService ?? throw new ArgumentNullException($"{nameof(userService)} is null ");
            _userConfigurationRolesTenantService = userConfigurationRolesTenantService ?? throw new ArgumentNullException($"{nameof(userConfigurationRolesTenantService)} is null ");
            _rolTenantService = rolTenantService ?? throw new ArgumentNullException($"{nameof(rolTenantService)} is null ");
        }

        public async Task<ResultDto<AccessUserDto>> GetUserById(string userId)
        {
            AccessUserDto result = null;
            Users user = await _identityService.FindById(userId);

            if (user != null)
            {
                string userRol = _userConfigurationRolesTenantService.GetRoleIdByUserId(user.UserConfigurationId.Value);
                result = user.ToAccessUserDto(userRol);
            }
            else
            {
                result = new AccessUserDto() { };
            }

            return ProcessResult(result, result != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public async Task CreateNewUser(GlobalPeopleDto globalPeople, ErrorsDto errors)
        {
            Users user = globalPeople.AccessUserDto.ToEntity(globalPeople.PeopleDto);
            IdentityResult CreateUserTask = await _identityService.CreateNewUser(user, globalPeople.AccessUserDto.Password);

            if (CreateUserTask.Succeeded)
            {
                if(globalPeople.AccessUserDto != null)
                {
                    UserConfigurationRolesTenantDto userConfigurationRolesTenant = new UserConfigurationRolesTenantDto()
                    {
                        UserConfigurationId = globalPeople.AccessUserDto.UserConfigurationId,
                        RolesTenantId = globalPeople.AccessUserDto.SelectedRol
                    };
                    var resultRolTenant = _userConfigurationRolesTenantService.CreateUserConfigurationRolTenant(userConfigurationRolesTenant);
                    if(resultRolTenant.Errors != null)
                    {
                        errors.AddError(new ErrorDto() { ErrorType = ErrorType.EntityNotExists, ErrorMessageKey = PeopleConstants.PeopleNotCreated }); //TODO: Carmen. Personalizar mensaje
                    }
                }
            }
            else
            {
                errors.AddError(new ErrorDto() { ErrorType = ErrorType.EntityNotExists, ErrorMessageKey = PeopleConstants.PeopleNotCreated });
            }
        }

        public async Task UpdateUser(GlobalPeopleDto globalPeople, Users user, ErrorsDto errors)
        {
            if (!string.IsNullOrEmpty(globalPeople.AccessUserDto.Password))
            {
                globalPeople.AccessUserDto.Password = GetHashPassword(user, globalPeople.AccessUserDto.Password);
            }
            user = globalPeople.AccessUserDto.ToUpdateEntity(globalPeople.PeopleDto, user);

            IdentityResult identityResult = await _identityService.UpdateUser(user);
            if (identityResult.Succeeded)
            {
                if (!string.IsNullOrEmpty(globalPeople.AccessUserDto.SelectedRol))
                {
                    string userRol = _userConfigurationRolesTenantService.GetRoleIdByUserId(user.UserConfigurationId.Value);
                    var userConfigurationRolesTenantDto = new UserConfigurationRolesTenantDto()
                    {
                        UserConfigurationId = user.UserConfigurationId.Value,
                        RolesTenantId = globalPeople.AccessUserDto.SelectedRol
                    };
                    if (userRol != null)
                    {
                        if (userRol != globalPeople.AccessUserDto.SelectedRol)
                        {
                            _userConfigurationRolesTenantService.DeleteUserConfigurationRolTenant(user.UserConfigurationId.Value);
                            _userConfigurationRolesTenantService.CreateUserConfigurationRolTenant(userConfigurationRolesTenantDto);
                        }
                    }
                    else
                    {                        
                        _userConfigurationRolesTenantService.CreateUserConfigurationRolTenant(userConfigurationRolesTenantDto);
                    }
                }
            }
            else
            {
                errors.AddError(new ErrorDto() { ErrorType = ErrorType.EntityNotExists, ErrorMessageKey = PeopleConstants.PeopleNotUpdated });
            }
        }

        public ResultDto<bool> DeleteUser(int userConfigurationId, ErrorsDto errors)
        {
            var result = _userService.DeleteUser(userConfigurationId);

            return ProcessResult(result.Data, result != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public async Task SaveAccessUser(GlobalPeopleDto globalPeople, ErrorsDto errors)
        {
            if (errors.Errors == null)
            {
                var validations = await ValidateAccessUser(globalPeople.AccessUserDto, null);
                if (validations.Any())
                {
                    errors.AddRangeError(validations);
                }
                else
                {
                    if (globalPeople.AccessUserDto.Id == default(Guid))
                    {
                        if (globalPeople.AccessUserDto.AccessUserWithData())
                        {
                            await CreateNewUser(globalPeople, errors);
                        }
                    }
                    else
                    {
                        Users user = await _identityService.FindById(globalPeople.AccessUserDto.Id.ToString());

                        if (user != null)
                        {
                            await UpdateUser(globalPeople, user, errors);
                        }
                        else
                        {
                            errors.AddError(new ErrorDto() { ErrorType = ErrorType.EntityAlredyExists, ErrorMessageKey = PeopleConstants.PeopleNotExist });
                        }
                    }
                }
            }
        }

        public async Task<List<ErrorDto>> ValidateAccessUser(AccessUserDto accessUser, Users user)
        {
            List<ErrorDto> errorResult = new List<ErrorDto>();
            IdentityResult result = new IdentityResult();

            if (accessUser.AccessUserWithData())
            {
                bool noPassword = true;

                if (!string.IsNullOrEmpty(accessUser.Password))
                {
                    result = await _identityService.ValidatePassword(user, accessUser.Password);
                    noPassword = false;
                }

                if (result.Succeeded || noPassword)
                {
                    if (!accessUser.IsValid())
                    {
                        errorResult.Add(new ErrorDto()
                        {
                            ErrorType = ErrorType.ValidationError,
                        });
                    }
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        errorResult.Add(new ErrorDto()
                        {
                            ErrorType = ErrorType.ValidationError,
                            ErrorMessageKey = error.Description
                        });
                    }
                }
            }

            return errorResult;
        }

        public string GetHashPassword(Users user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }
    }
}