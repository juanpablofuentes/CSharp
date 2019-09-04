using Group.Salto.Entities;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Identity;
using Group.Salto.ServiceLibrary.Common.Dtos.Templates;
using Group.Salto.ServiceLibrary.Common.Dtos.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.User;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Idantity
{
    public interface IIdentityService
    {
        Task<ResultDto<IdentityResultDto<UserApiDto>>> SignInToken(IdentityLoginDto login);
        Task<ResultDto<IdentityResultDto<IdentityLoginDto>>> SignIn(IdentityLoginDto login);
        Task<ResultDto<UserDto>> Create(UserDto user, string password);
        Task<ResultDto<UserDto>> FindByEmail(string email);
        Task<IdentityResult> CreateNewUser(Users user, string password);
        Task<IdentityResult> UpdateUser(Users user);
        Task<Users> FindById(string Id);
        Task<Users> FindByUserName(string userName);
        Task<string> GetUserRole(Users user);
        Task<IdentityResult> AddToRole(Users user, string rolName);
        Task<IdentityResult> RemoveFomRole(Users user, string rolName);
        Task<bool> ExistUsersInRol(string rolName);
        Task<IdentityResult> ValidatePassword(Users user, string password);      
        Task<ResultDto<UserDto>> FindUserOptionById(string Id);
        Task<ErrorDto> UpdateUser(UserOptionsDto userOptionsDto);
        Task<IdentityResult> CreateNewRol(Roles rol);
        Task<IdentityResult> UpdateRol(Roles rol);
        Task<IdentityResult> DeleteRol(Roles rol);
        IQueryable<Roles> GetAllRoles();
        //TODO: Carmen.RolesActionGroupsActions
        Task<Roles> GetRolById(string Id);
        int GetMaxId();
        Task ForgotPassword(IdentityForgotPasswordDto forgotPasswordData, TemplateForgotPasswordDto templateForgotPasswordData);
        Task<ResultDto<IdentityResultDto<IdentityResetPasswordDto>>> ResetPassword(IdentityResetPasswordDto identityResetPasswordDto);
        Task<Users> FindByIdIncludeAll(string id);
        Users GetByUserConfigurationId(int userConfigurationId);
        IQueryable<Users> GetAllByCusomterNotDeleted(Guid tenantId);
        Task<string> GetTokenPasswordReset(string email);
        Task<Dictionary<int, string>> GetAllRolesKeyValues();
        Task<IList<Roles>> GetAllRolesById(IEnumerable<string> ids);
        Task RefreshSignIn(string id);
        Task SignOut();
    }
}