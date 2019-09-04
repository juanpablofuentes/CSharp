using Group.Salto.Common.Constants;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace Group.Salto.ServiceLibrary.Helpers
{
    public static class DateTimeZoneHelper
    {
        private static IHttpContextAccessor _httpContextAccessor;

        public static void SetHttpContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException($"{nameof(IHttpContextAccessor)} is null");
        }

        public static DateTime ToLocalTimeByUser(DateTime date)
        {
            TimeZoneInfo timeZone = GetTimeZone();
            return TimeZoneInfo.ConvertTimeFromUtc(date, timeZone);
        }

        public static DateTime ToUtcByUser(DateTime date)
        {
            TimeZoneInfo timeZone = GetTimeZone();
            return TimeZoneInfo.ConvertTimeToUtc(date, timeZone);
        }

        private static TimeZoneInfo GetTimeZone()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            string timeZoneId = (httpContext?.User?.Identity as ClaimsIdentity).FindFirst(AppIdentityClaims.TimeZoneId).Value;
            return TimeZoneInfo.GetSystemTimeZones().First(x => x.Id == timeZoneId);
        }
    }
}
