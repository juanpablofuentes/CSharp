using Group.Salto.Common.Constants;
using Group.Salto.ServiceLibrary.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading;

namespace Group.Salto.SOM.Web.Extensions
{
    public static class LocalizedExtensions
    {
        public static string GetUILocalizedText(string key)
        {
            return TranslationHelper.GetText(key, Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName);
        }
        public static string GetTranslationsText(this string key)
        {
            return GetUILocalizedText(key);
        }
        public static string GetLocalizedText(this IHtmlHelper context, string key)
        {
            var languageCode = context.GetLanguageCookie();
            return TranslationHelper.GetText(key, languageCode);
        }

        public static string GetLanguageCookie(this IHtmlHelper context)
        {
            var languageCode = context.ViewContext.HttpContext.Request.GetCookie(AppConstants.CookieLanguageConstant);
            languageCode = !string.IsNullOrEmpty(languageCode)
                ? languageCode
                : Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
            return languageCode;
        }
    }
}
