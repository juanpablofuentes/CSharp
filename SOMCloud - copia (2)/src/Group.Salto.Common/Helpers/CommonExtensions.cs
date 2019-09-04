using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Common.Helpers
{
    public static class CommonExtensions
    {
        public static string RemoveBlankSpaces(this string source)
        {
            return source?.Trim()?.Replace(" ", "");
        }

        public static IList<Guid> ToGuids(this string elements, string separator)
        {
            return elements?.Split(separator).Select(x => Guid.Parse(x)).ToList();
        }

        public static T ParseEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static string ToString(this int[] items, string separator)
        {
            return string.Join(separator, items.Select(p => p.ToString()).ToArray());
        }
    }
}