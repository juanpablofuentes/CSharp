using Group.Salto.Common;
using Group.Salto.Common.Constants;
using Group.Salto.Common.Enums;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Helpers;
using Group.Salto.SOM.Web.Extensions;
using Group.Salto.SOM.Web.Models;
using Group.Salto.SOM.Web.Models.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using Microsoft.AspNetCore.Http;

namespace Group.Salto.SOM.Web.Controllers
{
    public class BaseAPIController : Controller
    {
        private readonly CookiesConfiguration _configuration;
        protected ILoggingService LoggingService { get; }

        public BaseAPIController(ILoggingService loggingService, IConfiguration configuration, IHttpContextAccessor accessor)
        {
            LoggingService = loggingService ?? throw new ArgumentNullException(nameof(ILoggingService));
            _configuration = configuration.GetSection(AppsettingsKeys.CookiesConfiguration)
                             .Get<CookiesConfiguration>() ??
                             throw new ArgumentNullException(nameof(CookiesConfiguration));
            SetSystemLanguage(accessor);
        }

        private void SetSystemLanguage(IHttpContextAccessor accessor)
        {
            var cookie = accessor.HttpContext?.Request?.GetCookie(AppConstants.CookieLanguageConstant);
            SetThreadCulture(cookie);
        }

        protected IActionResult SetLanguage(string culture, string returnUrl)
        {
            this.SetCookie(AppConstants.CookieLanguageConstant, culture, _configuration.ExpirationHours);
            return LocalRedirect(returnUrl);
        }

        protected void SetLanguage(string culture)
        {
            if (!string.IsNullOrEmpty(culture))
            {
                this.SetCookie(AppConstants.CookieLanguageConstant, culture, _configuration.ExpirationHours);
                SetThreadCulture(culture);
            }
        }

        protected void SetThreadCulture(string culture)
        {
            if (!string.IsNullOrEmpty(culture))
            {
                CultureInfo fullCulture = CultureInfo.GetCultures(CultureTypes.AllCultures).First(x => x.TwoLetterISOLanguageName.ToUpper() == culture.ToUpper());
                Thread.CurrentThread.CurrentUICulture = fullCulture;
                Thread.CurrentThread.CurrentCulture = fullCulture;
            }
        }

        protected int GetConfigurationUserId()
        {
            var result = 0;
            int.TryParse((HttpContext?.User?.Identity as ClaimsIdentity)
                ?.FindFirst(AppIdentityClaims.UserId)?.Value, out result);
            return result;
        }
    }
}