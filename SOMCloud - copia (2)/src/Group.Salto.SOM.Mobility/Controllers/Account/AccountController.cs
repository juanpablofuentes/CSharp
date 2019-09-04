using System.IO;
using System.Threading.Tasks;
using Group.Salto.Common;
using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.Templates;
using Group.Salto.Common.Enums;
using Group.Salto.ServiceLibrary.Common.Contracts.Idantity;
using Group.Salto.ServiceLibrary.Common.Dtos.Identity;
using Group.Salto.ServiceLibrary.Common.Dtos.Templates;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Account;
using Group.Salto.SOM.Mobility.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Group.Salto.SOM.Mobility.Controllers.Account
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IIdentityService _identityService;
        private readonly IHostingEnvironment _hostingEnv;
        private readonly IConfiguration _settingsConf;

        public AccountController(IConfiguration configuration,
            IIdentityService identityService,
            IHostingEnvironment hostingEnv) : base(configuration)
        {
            _identityService = identityService;
            _hostingEnv = hostingEnv;
            _settingsConf = configuration;
        }

        [HttpPost]
        [ActionName("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var token = await _identityService.GetTokenPasswordReset(changePasswordDto.UserName);
            var user = await _identityService.FindByEmail(changePasswordDto.UserName);
            if (user != null)
            {
                var resetPassDto = new IdentityResetPasswordDto()
                {
                    UserId = user.Data.Id,
                    Token = token,
                    Password = changePasswordDto.NewPassword
                };

                var result = await _identityService.ResetPassword(resetPassDto);
                return Ok(result.Data.Result == IdentityResultEnum.Success);
            }
            return Ok(false);

        }
        
        [HttpPost]
        [ActionName("RecoverPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordDto recoverPasswordDto)
        {
            var hosting = _settingsConf.GetSection(AppsettingsKeys.ForgotPasswordLink).GetValue<string>(AppsettingsKeys.Hosting);
            var url = _settingsConf.GetSection(AppsettingsKeys.ForgotPasswordLink).GetValue<string>(AppsettingsKeys.Url);
            var forgotPassword = new IdentityForgotPasswordDto()
            {
                Subject = LocalizedExtensions.GetUiLocalizedText(AccountConstants.ForgotPasswordEmailSubject),
                EmailTo = recoverPasswordDto.EmailUser,
                CallbackUrl = $"{ hosting }/{ url }"
            };
            TemplateForgotPasswordDto templateForgotPasswordDto = GenerateTemplateDto();
            await _identityService.ForgotPassword(forgotPassword, templateForgotPasswordDto);
            return Ok(true);
        }

        //TODO: Refactor
        private TemplateForgotPasswordDto GenerateTemplateDto()
        {
            TemplateForgotPasswordDto dto = new TemplateForgotPasswordDto();
            dto.ImageUrl = "http://som.esalto.com/saltocsfrontend//img/logo_som.png";
            dto.template = GetTemplate();
            dto.LongForgetMailConsiderations = LocalizedExtensions.GetUiLocalizedText(TemplateForgotPasswordConstants.TemplateForgotPasswordLongForgetMailConsiderations);
            dto.LongForgetMailFarewell = LocalizedExtensions.GetUiLocalizedText(TemplateForgotPasswordConstants.TemplateForgotPasswordLongForgetMailFarewell);
            dto.LongForgetMailFooter = LocalizedExtensions.GetUiLocalizedText(TemplateForgotPasswordConstants.TemplateForgotPasswordLongForgetMailFooter);
            dto.LongForgetMailGreet = LocalizedExtensions.GetUiLocalizedText(TemplateForgotPasswordConstants.TemplateForgotPasswordLongForgetMailGreet);
            dto.LongForgetMailLink = LocalizedExtensions.GetUiLocalizedText(TemplateForgotPasswordConstants.TemplateForgotPasswordLongForgetMailLink);
            dto.LongForgetMailTitle = LocalizedExtensions.GetUiLocalizedText(TemplateForgotPasswordConstants.TemplateForgotPasswordLongForgetMailTitle);
            dto.LegalInfo = LocalizedExtensions.GetUiLocalizedText(TemplateForgotPasswordConstants.TemplateForgotPasswordLegalInfo);
            dto.Privacy = LocalizedExtensions.GetUiLocalizedText(TemplateForgotPasswordConstants.TemplateForgotPasswordPrivacy);
            return dto;
        }

        //TODO: Refactor
        public string GetTemplate()
        {
            string path = Path.Combine(_hostingEnv.ContentRootPath, "Templates");
            string fullPath = path + @"\ForgetPasswordMailTemplate.html";
            return System.IO.File.ReadAllText(fullPath);
        }
    }
}
