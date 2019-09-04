using System;
using Group.Salto.Common.Constants;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Language;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Group.Salto.SOM.Web.Controllers
{
    public class BaseLanguageController : BaseController
    {
        protected ILanguageService LanguageService { get; private set; }

        public BaseLanguageController(ILoggingService loggingService,
                                        IConfiguration configuration,
                                        IHttpContextAccessor accessor,
                                        ILanguageService languageService) : base(loggingService, configuration, accessor)
        {
            LanguageService = languageService ?? throw new ArgumentNullException($"{nameof(ILanguageService)} is null");
        }

        protected int GetLanguageId()
        {
            var culture = GetCookies(AppConstants.CookieLanguageConstant);
            int languageId = !string.IsNullOrEmpty(culture)
                ? LanguageService.GetByCulture(culture).Data.Id
                : LanguageService.GetByCulture(AppConstants.CultureTwoLettersSpanish).Data.Id;
            return languageId;
        }
    }
}