using System;
using System.Globalization;

namespace Group.Salto.Common.Helpers
{
    public static class DecimalConverter
    {
        public static string DecimalToString(this decimal source, string culture)
        {
            return source.ToString(CultureInfo.CreateSpecificCulture(culture));
        }

        public static bool TryParseToDecimal(this string source, string culture, out decimal result)
        {
            bool parsed = true;
            result = 0;
            try
            {
                result = decimal.Parse(source, CultureInfo.CreateSpecificCulture(culture));
            }
            catch (Exception)
            {
                parsed = false;
            }
            return parsed;
        }

        public static bool IsDecimalValue(this string source, string culture)
        {
            bool parsed = true;
            try
            {
                decimal.Parse(source, CultureInfo.CreateSpecificCulture(culture));
            }
            catch (Exception)
            {
                parsed = false;
            }
            return parsed;
        }

        public static decimal StringToDecimal(this string source, string culture)
        {
            decimal decimalConverted = 0;
            try
            {
                decimalConverted = decimal.Parse(source, CultureInfo.CreateSpecificCulture(culture));
            }
            catch (Exception)
            {
            }

            return decimalConverted;
        }
    }
}
