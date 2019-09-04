using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Group.Salto.SOM.Web.Extensions
{
    public static class ControllerBaseCookiesExtensions
    {
        public static void SetCookie(this ControllerBase controller, string key, string value, int hours)
        {
            CookieOptions option = new CookieOptions {Expires = DateTime.Now.AddHours(hours) };
            controller.Response.Cookies.Append(key, value, option);
        }

        public static string GetCookie(this ControllerBase controller, string key)
        {
            if (controller.Request != null && controller.Request.Cookies.TryGetValue(key, out var result))
            {
                return result;
            }
            return string.Empty;
        }

        public static void RemoveCookie(this ControllerBase controller, string key)
        {
            controller.Response.Cookies.Delete(key);
        }

        public static string GetCookie(this HttpRequest request, string key)
        {
            if (request.Cookies.TryGetValue(key, out var result))
            {
                return result;
            }
            return string.Empty;
        }
    }
}
