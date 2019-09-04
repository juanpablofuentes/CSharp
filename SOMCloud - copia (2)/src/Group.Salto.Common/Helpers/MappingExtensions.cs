using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Common.Helpers
{
    public static class MappingExtensions
    {
        public static IList<TInput> MapList<TInput, TOutput>(this IList<TOutput> source, Func<TOutput, TInput> replaceFunc)
        {
            var result = source?.Select(replaceFunc.Invoke);
            return result?.ToList();
        }

        public static IEnumerable<TInput> MapList<TInput, TOutput>(this IEnumerable<TOutput> source, Func<TOutput, TInput> replaceFunc)
        {
            var result = source?.Select(replaceFunc.Invoke);
            return result?.ToList();
        }

        public static IList<TInput> MapList<TInput, TOutput>(this IQueryable<TOutput> source, Func<TOutput, TInput> replaceFunc)
        {
            var result = source?.Select(replaceFunc.Invoke);
            return result?.ToList();
        }
    }
}
