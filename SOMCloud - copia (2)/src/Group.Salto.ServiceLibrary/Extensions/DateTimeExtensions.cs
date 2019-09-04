using System;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool EqualsUntilMinute(this DateTime? dt1, DateTime? dt2)
        {
            bool result;
            if (dt1 == null && dt2 == null)
            {
                result = true;
            }
            else if (dt1 == null || dt2 == null)
            {
                result = false;
            }
            else
            {
                result = dt1?.Year == dt2?.Year && dt1?.Month == dt2?.Month && dt1?.Day == dt2?.Day && dt1?.Hour == dt2?.Hour && dt1?.Minute == dt2?.Minute;
            }
            return result;
        }
    }
}
