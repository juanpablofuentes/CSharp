using Group.Salto.Common.Enums;
using Group.Salto.Entities;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using Group.Salto.ServiceLibrary.Common.Contracts.Idantity;
using Group.Salto.ServiceLibrary.Common.Contracts.Notification;
using Group.Salto.ServiceLibrary.Common.Contracts.Templates;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Identity;
using Group.Salto.ServiceLibrary.Common.Dtos.Notification;
using Group.Salto.ServiceLibrary.Common.Dtos.Templates;
using Group.Salto.ServiceLibrary.Common.Dtos.User;
using Group.Salto.ServiceLibrary.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.User;

namespace Group.Salto.ServiceLibrary.Implementations.Identity
{
    public class IdentityService : IIdentityService
    {
        protected readonly UserManager<Users> _userManager;
        protected readonly SignInManager<Users> _signInManager;
        protected readonly RoleManager<Roles> _roleManager;
        protected readonly IPasswordValidator<Users> _passwordValidator;
        protected readonly INotificationFactory _notificationFactory;
        protected readonly INotificationConfigurationService _notificationConfigurationServiceConfiguration;
        protected readonly IUserRepository _userRepository;
        protected readonly ITemplateForgotPasswordService _templateForgotPasswordService;

        public IdentityService(UserManager<Users> userManager,
            SignInManager<Users> signInManager,
            IPasswordValidator<Users> passwordValidator,
            INotificationFactory notificationFactory,
            INotificationConfigurationService notificationConfigurationServiceConfiguration,
            IUserRepository userRepository,
            RoleManager<Roles> roleManager,
            ITemplateForgotPasswordService templateForgotPasswordService)
        {
            _userManager = userManager ?? throw new ArgumentNullException($"{nameof(userManager)} is null ");
            _signInManager = signInManager ?? throw new ArgumentNullException($"{nameof(signInManager)} is null");
            _passwordValidator = passwordValidator ?? throw new ArgumentNullException($"{nameof(passwordValidator)} is null ");
            _notificationFactory = notificationFactory ?? throw new ArgumentNullException($"{nameof(notificationFactory)} is null ");
            _notificationConfigurationServiceConfiguration = notificationConfigurationServiceConfiguration ?? throw new ArgumentNullException($"{nameof(notificationConfigurationServiceConfiguration)} is null ");
            _userRepository = userRepository ?? throw new ArgumentNullException($"{nameof(userRepository)} is null ");
            _roleManager = roleManager ?? throw new ArgumentNullException($"{nameof(roleManager)} is null ");
            _templateForgotPasswordService = templateForgotPasswordService ?? throw new ArgumentNullException($"{nameof(templateForgotPasswordService)} is null ");
        }

        public async Task<ResultDto<UserDto>> Create(UserDto user, string password)
        {

            var usr = user.ToEntity();
            var res = await _userManager.CreateAsync(usr, password);
            return new ResultDto<UserDto>() { Data = res.Succeeded ? usr.ToUserDto() : null };
        }

        public async Task<ResultDto<UserDto>> FindByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            ResultDto<UserDto> res = new ResultDto<UserDto>();
            if (user != null)
            {
                res.Data = new UserDto();
                res.Data = user.ToUserDto();
            }
            return res;
        }

        public async Task<ResultDto<IdentityResultDto<UserApiDto>>> SignInToken(IdentityLoginDto login)
        {
            IdentityResultDto<UserApiDto> res = new IdentityResultDto<UserApiDto>();
            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, login.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var user = await _userManager.Users.Include(u => u.Language).SingleAsync(u => u.Email == login.Email);
                res.Result = user.IsActive ? IdentityResultEnum.Success : IdentityResultEnum.Disabled;
                res.LanguageCode = user.Language.CultureCode;
                res.Data = user.ToApiDto();
            }
            else if (result.IsLockedOut)
            {
                res.Result = IdentityResultEnum.Locked;
            }
            else
            {
                res.Result = IdentityResultEnum.Failed;
            }

            return new ResultDto<IdentityResultDto<UserApiDto>>() { Data = res };
        }

        public async Task<ResultDto<IdentityResultDto<IdentityLoginDto>>> SignIn(IdentityLoginDto login)
        {
            IdentityResultDto<IdentityLoginDto> res = new IdentityResultDto<IdentityLoginDto>();
            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, login.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var user = await _userManager.Users.Include(u => u.Language).SingleAsync(u => u.Email == login.Email);
                res.Result = user.IsActive ? IdentityResultEnum.Success : IdentityResultEnum.Disabled;
                res.LanguageCode = user.Language.CultureCode;
                res.Data = new IdentityLoginDto() { UserConfigurationId = user.UserConfigurationId };
            }
            else if (result.IsLockedOut)
            {
                res.Result = IdentityResultEnum.Locked;
            }
            else
            {
                res.Result = IdentityResultEnum.Failed;
                res.Data = login;
            }

            return new ResultDto<IdentityResultDto<IdentityLoginDto>>() { Data = res };
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();            
        }

        public async Task<IdentityResult> CreateNewUser(Users user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> UpdateUser(Users user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task<Users> FindById(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            return user;
        }

        public async Task<Users> FindByIdIncludeAll(string id)
        {
            var user = await _userManager.Users
                                .Include(u=>u.Language)
                                .Include(u=>u.Customer).SingleOrDefaultAsync(x=>x.Id == id);
            return user;
        }

        public async Task<Users> FindByUserName(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            return user;
        }

        public async Task<string> GetUserRole(Users user)
        {
            IList<string> listOfRoles = await _userManager.GetRolesAsync(user);
            string userRol = listOfRoles.FirstOrDefault();

            return userRol;
        }

        public async Task<IdentityResult> AddToRole(Users user, string rolName)
        {
            return await _userManager.AddToRoleAsync(user, rolName);
        }

        public async Task<IdentityResult> RemoveFomRole(Users user, string rolName)
        {
            return await _userManager.RemoveFromRoleAsync(user, rolName);
        }

        public async Task<bool> ExistUsersInRol(string rolName)
        {
            IList<Users> result = await _userManager.GetUsersInRoleAsync(rolName);
            return result != null && result.Any();
        }

        public async Task<IdentityResult> ValidatePassword(Users user, string password)
        {
            return await _passwordValidator.ValidateAsync(_userManager, user, password);
        }

        public async Task<ErrorDto> UpdateUser(UserOptionsDto userOptionsDto)
        {
            ErrorDto errorDto = new ErrorDto();
            var user = await _userManager.FindByIdAsync(userOptionsDto.Id);
            if (!string.IsNullOrEmpty(userOptionsDto.Password))
            {
                var checkPassword = await _userManager.CheckPasswordAsync(user, userOptionsDto.OldPassword);
                if (!checkPassword) errorDto.ErrorMessageKey = "UserOptionsValidationCheckOldPassword";
                else
                {
                    var validateNewPassword = await _passwordValidator.ValidateAsync(_userManager, user, userOptionsDto.Password);
                    if (validateNewPassword.Errors != null && validateNewPassword.Errors.Any()) errorDto.ErrorMessageKey = "UserOptionsValidationFormatNewPassword";
                }
                if (!string.IsNullOrEmpty(errorDto.ErrorMessageKey)) return errorDto;

                var newPassword = _userManager.PasswordHasher.HashPassword(user, userOptionsDto.Password);
                user.PasswordHash = newPassword;
            }

            user.LanguageId = userOptionsDto.LanguageId;
            user.NumberEntriesPerPage = userOptionsDto.NumberEntriesPerPage;
            IdentityResult result = await _userManager.UpdateAsync(user);
            if (result.Errors.Any()) errorDto.ErrorMessageKey = "FormErrorsMessage";
            return errorDto;
        }

        public async Task<ResultDto<UserDto>> FindUserOptionById(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            ResultDto<UserDto> res = new ResultDto<UserDto>();
            if (user != null)
            {
                res.Data = user.ToUserDto();
            }
            return res;
        }

        public async Task ForgotPassword(IdentityForgotPasswordDto forgotPasswordData, TemplateForgotPasswordDto templateForgotPasswordData)
        {
            Users user = await _userManager.Users.Include(u => u.Customer).SingleOrDefaultAsync(u => u.Email == forgotPasswordData.EmailTo);
            if (user != null)
            {                
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var connectionString = user.Customer.ConnString;                
                NotificationTypeEnum notificationType = _notificationConfigurationServiceConfiguration.GetNotificationTypeConfiguration(connectionString);                
                if (notificationType != NotificationTypeEnum.Empty)
                {
                    var notificationService = _notificationFactory.GetService(notificationType);
                    templateForgotPasswordData.Href = string.Format(forgotPasswordData.CallbackUrl, user.Id, token);
                    var body = _templateForgotPasswordService.GetMailBody(templateForgotPasswordData);

                    var emailMessage = new EmailMessageDto()
                    {
                        ConnectionString = connectionString,
                        To = forgotPasswordData.EmailTo,
                        Subject = forgotPasswordData.Subject,
                        Body = body
                    };
                    notificationService.SendNotification(emailMessage);
                }                
            }
        }

        public async Task<ResultDto<IdentityResultDto<IdentityResetPasswordDto>>> ResetPassword(IdentityResetPasswordDto identityResetPasswordDto)
        {
            IdentityResultDto<IdentityResetPasswordDto> res = new IdentityResultDto<IdentityResetPasswordDto>();
            var usr = await _userManager.FindByIdAsync(identityResetPasswordDto.UserId);
            var tokenWithPlus = identityResetPasswordDto.Token.Replace(" ", "+");
            IdentityResult result = await _userManager.ResetPasswordAsync(usr, tokenWithPlus, identityResetPasswordDto.Password);

            if (result.Succeeded)
            {
                res.Result = IdentityResultEnum.Success;
            }
            else if (result.Errors.Any())
            {
                res.Result = IdentityResultEnum.Locked;
            }
            else
            {
                res.Result = IdentityResultEnum.Failed;
            }

            return new ResultDto<IdentityResultDto<IdentityResetPasswordDto>>() { Data = res };
        }        

        public async Task<IdentityResult> CreateNewRol(Roles rol)
        {
            return await _roleManager.CreateAsync(rol);
        }

        public async Task<IdentityResult> UpdateRol(Roles rol)
        {
            return await _roleManager.UpdateAsync(rol);
        }

        public async Task<IdentityResult> DeleteRol(Roles rol)
        {
            return await _roleManager.DeleteAsync(rol);
        }

        public IQueryable<Roles> GetAllRoles()
        {
            return _roleManager.Roles;
        }

        //TODO: Carmen. RolesActionGroupsActions
        public async Task<Roles> GetRolById(string Id)
        {
            return await _roleManager.Roles.Include(x => x.ActionsRoles).SingleAsync(x => x.Id == Id);
        }

        public async Task<Dictionary<int, string>> GetAllRolesKeyValues()
        {
            return await _roleManager.Roles
                .Select(s => new { s.Id, s.Name })
                .ToDictionaryAsync(t => Convert.ToInt32(t.Id), t => t.Name);
        }

        public async Task<IList<Roles>> GetAllRolesById(IEnumerable<string> ids)
        {
            return await _roleManager.Roles.Where(x => ids.Any(k => k == x.Id)).ToListAsync();
        }

        public int GetMaxId()
        {
            return _roleManager.Roles.Select(s => Convert.ToInt32(s.Id)).Max();
        }

        public Users GetByUserConfigurationId(int userConfigurationId)
        {
            return _userManager.Users.Where(x => x.UserConfigurationId == userConfigurationId && !x.IsDeleted).FirstOrDefault();
        }

        public IQueryable<Users> GetAllByCusomterNotDeleted(Guid tenantId)
        {
            return _userManager.Users
                .Where(x => x.CustomerId == tenantId && !x.IsDeleted)
                .Select(s => new Users { Id = s.Id, UserName = s.UserName, UserConfigurationId = s.UserConfigurationId });
        }

        public async Task<string> GetTokenPasswordReset(string email)
        {
            var user = await _userManager.Users.SingleAsync(u => u.Email == email);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return token;
        }

        public async Task RefreshSignIn(string id)
        {
            var user = await _userManager.Users.SingleAsync(u => u.Id == id);
            await _signInManager.RefreshSignInAsync(user);
        }
    }
}