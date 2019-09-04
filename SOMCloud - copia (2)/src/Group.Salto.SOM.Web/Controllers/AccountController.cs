using Group.Salto.Common;
using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.Templates;
using Group.Salto.Common.Enums;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Idantity;
using Group.Salto.ServiceLibrary.Common.Dtos.Identity;
using Group.Salto.ServiceLibrary.Common.Dtos.Templates;
using Group.Salto.SOM.Web.Extensions;
using Group.Salto.SOM.Web.Models.Account;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Result;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using Group.Salto.ServiceLibrary.Common.Contracts.RolesActionGroupsActionsTenant;
using System.Security.Claims;

namespace Group.Salto.SOM.Web.Controllers
{
    public class AccountController : IdentityController
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnv;        
        private readonly IHttpContextAccessor _accessor;
        private readonly IRolesActionGroupsActionsTenantAdapter _rolesActionGroupsActionsTenantAdapter;

        public AccountController(
                IIdentityService identityService,              
                ILoggingService loggingService,
                IConfiguration configuration,
                IHttpContextAccessor accessor,
                IHostingEnvironment hostingEnv,
                IRolesActionGroupsActionsTenantAdapter rolesActionGroupsActionsTenantAdapter) : base(identityService, loggingService,accessor, configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException($"{nameof(IConfiguration)} is null");
            _hostingEnv = hostingEnv ?? throw new ArgumentNullException($"{nameof(IHostingEnvironment)} is null");
            _accessor = accessor ?? throw new ArgumentNullException($"{nameof(IHttpContextAccessor)} is null");
            _rolesActionGroupsActionsTenantAdapter = rolesActionGroupsActionsTenantAdapter ?? throw new ArgumentNullException($"{nameof(IRolesActionGroupsActionsTenantAdapter)} is null");
        }

        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        public IActionResult LogOff()
        {
            _identityService.SignOut();
            return View(nameof(Login));
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        public IActionResult ResetPassword(string userId, string code)
        {
            ResultViewModel<ResetPasswordViewModel> model = new ResultViewModel<ResetPasswordViewModel>();
            model.Data = new ResetPasswordViewModel();
            model.Data.UserId = userId;
            model.Data.Token = code;
            return View("ResetPassword", model);
        }
        
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ResultViewModel<ForgotPasswordViewModel> model)
        {
            if (ModelState.IsValid)
            {
                var hosting = _configuration.GetSection(AppsettingsKeys.ForgotPasswordLink).GetValue<string>(AppsettingsKeys.Hosting);
                var url = _configuration.GetSection(AppsettingsKeys.ForgotPasswordLink).GetValue<string>(AppsettingsKeys.Url);
                var forgotPassword = new IdentityForgotPasswordDto()
                {
                    Subject = LocalizedExtensions.GetUILocalizedText(AccountConstants.ForgotPasswordEmailSubject),
                    EmailTo = model.Data.Email,
                    CallbackUrl = $"{ hosting }/{ url }"
                };
                TemplateForgotPasswordDto templateForgotPasswordDto = GenerateTemplateDto();
                await _identityService.ForgotPassword(forgotPassword, templateForgotPasswordDto);
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResultViewModel<ResetPasswordViewModel> model)
        {
            if (ModelState.IsValid)
            {
                var result = await _identityService.ResetPassword(model.Data.ToIdentityLoginDto());
                if (result.Data.Result == IdentityResultEnum.Success) return View("Login");                
            }
            var failLoginResult = ProcessResult(model.Data, new List<FeedbackViewModel>() {
                        new FeedbackViewModel { FeedbackType = FeedbackTypeEnum.Error, IsFixed = false, MessageKey = "ResetPasswordFailMessage", TitleKey = "ResetPasswordFailMessage" } });
            return View(failLoginResult);
        }

        protected override IActionResult OnLoginSuccess()
        {
            return RedirectToAction("Index", "Home");
        }

        protected override IActionResult OnLoginIsLockedout()
        {
            throw new System.NotImplementedException();
        }

        protected override IActionResult OnLoginInvalid(ResultViewModel<LoginViewModel> login)
        {
            var failLoginResult = ProcessResult(login.Data, new List<FeedbackViewModel>() {
                new FeedbackViewModel { FeedbackType = Common.Enums.FeedbackTypeEnum.Error, IsFixed = false, TitleKey = AccountConstants.LoginFailTitle, MessageKey = LocalizationsConstants.FormErrorsMessage } });
            return View(failLoginResult);
        }

        protected override IActionResult OnLoginFailed(ResultViewModel<LoginViewModel> login)
        {
            var failLoginResult = ProcessResult(login.Data, new List<FeedbackViewModel>() {
                new FeedbackViewModel { FeedbackType = Common.Enums.FeedbackTypeEnum.Error, IsFixed = false, TitleKey = AccountConstants.LoginFailTitle, MessageKey = AccountConstants.LoginFailMessage } });
            return View(failLoginResult);
        }

        //TODO: Refactor
        private TemplateForgotPasswordDto GenerateTemplateDto()
        {
            TemplateForgotPasswordDto dto = new TemplateForgotPasswordDto();
            dto.ImageUrl = "http://som.esalto.com/saltocsfrontend//img/logo_som.png";
            dto.template = GetTemplate();
            dto.LongForgetMailConsiderations = LocalizedExtensions.GetUILocalizedText(TemplateForgotPasswordConstants.TemplateForgotPasswordLongForgetMailConsiderations);
            dto.LongForgetMailFarewell = LocalizedExtensions.GetUILocalizedText(TemplateForgotPasswordConstants.TemplateForgotPasswordLongForgetMailFarewell);
            dto.LongForgetMailFooter = LocalizedExtensions.GetUILocalizedText(TemplateForgotPasswordConstants.TemplateForgotPasswordLongForgetMailFooter);
            dto.LongForgetMailGreet = LocalizedExtensions.GetUILocalizedText(TemplateForgotPasswordConstants.TemplateForgotPasswordLongForgetMailGreet);
            dto.LongForgetMailLink = LocalizedExtensions.GetUILocalizedText(TemplateForgotPasswordConstants.TemplateForgotPasswordLongForgetMailLink);
            dto.LongForgetMailTitle = LocalizedExtensions.GetUILocalizedText(TemplateForgotPasswordConstants.TemplateForgotPasswordLongForgetMailTitle);
            dto.LegalInfo = LocalizedExtensions.GetUILocalizedText(TemplateForgotPasswordConstants.TemplateForgotPasswordLegalInfo);
            dto.Privacy = LocalizedExtensions.GetUILocalizedText(TemplateForgotPasswordConstants.TemplateForgotPasswordPrivacy);
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