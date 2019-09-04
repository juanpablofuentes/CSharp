using System;

namespace Group.Salto.MigrationData
{
    public static class MigrationHelper
    {
        public static DateTime? ToUtcDate(this DateTime? dateTime)
        {
            DateTime? result = null;
            if (dateTime.HasValue) result = dateTime.Value.ToUniversalTime();
            return result;
        }

        public static DateTime ToUtcDate(this DateTime dateTime)
        {
            return dateTime.ToUniversalTime();
        }
    }
}