using Group.Salto.Common.Constants;
using Group.Salto.Common.Enums;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Idantity;
using Group.Salto.SOM.Web.Models.Account;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Controllers
{
    public abstract class IdentityController : BaseController
    {
        protected readonly IIdentityService _identityService;

        protected IdentityController(IIdentityService identityService,
                                    ILoggingService loggingService,
                                    IHttpContextAccessor accessor,
                                    IConfiguration configuration) : base(loggingService, configuration, accessor)
        {
            _identityService = identityService ?? throw new ArgumentNullException($"{nameof(identityService)} is null");

        }

        [HttpPost]
        public async Task<IActionResult> Login(ResultViewModel<LoginViewModel> login)
        {
            if (ModelState.IsValid)
            {
                var result = await _identityService.SignIn(login.Data.ToIdentityLoginDto());
                if (result.Data.Result == IdentityResultEnum.Success)
                {
                    SetLanguage(result.Data.LanguageCode);
                    return OnLoginSuccess();
                }
                if (result.Data.Result == IdentityResultEnum.Locked)
                {
                    return OnLoginIsLockedout();
                }

                return OnLoginFailed(login);
            }
            return OnLoginInvalid(login);
        }

        protected virtual IActionResult OnLoginSuccess() => throw new NotImplementedException();
        protected virtual IActionResult OnLoginIsLockedout() => throw new NotImplementedException();
        protected virtual IActionResult OnLoginFailed(ResultViewModel<LoginViewModel> login) => throw new NotImplementedException();
        protected virtual IActionResult OnLoginInvalid(ResultViewModel<LoginViewModel> login) => throw new NotImplementedException();

        protected async Task RefreshUserLanguage()
        {
            var userId = GetUserIdClaim();
            var userInfo = await _identityService.FindByIdIncludeAll(userId);
            SetLanguage(userInfo.Language.CultureCode);
        }

        protected async Task RefreshSignIn()
        {
            var userId = GetUserIdClaim();
            await _identityService.RefreshSignIn(userId);
        }        
    }
}