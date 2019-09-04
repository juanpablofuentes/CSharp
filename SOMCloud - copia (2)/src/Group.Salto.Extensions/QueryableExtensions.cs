using System;
using System.Linq;
using System.Linq.Expressions;

namespace Group.Salto.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> MaybeSort<T, TR>(
            this IQueryable<T> source,
            string name,
            bool ascending,
            Expression<Func<T, TR>> prop)
        {
            return ExpressionExtensions<T>.AsPropertyName(prop) == name
                ? (ascending ? source.OrderBy(prop) : source.OrderByDescending(prop))
                : source;
        }

        public static IQueryable<T> WhereIfNotDefault<T, TR>(
            this IQueryable<T> source,
            TR value,
            Expression<Func<T, bool>> where)
        {
            if (typeof(TR) == typeof(string))
            {
                var svalue = value as string;
                return !string.IsNullOrEmpty(svalue) ? source.Where(where) : source;
            }
            if (typeof(TR) == typeof(Nullable<Guid>))
            {
                return value != null ? source.Where(where) : source;
            }
            var isNotDefault = !(ReferenceEquals(value, default(TR)) || value.Equals(default(TR)));
            return isNotDefault ? source.Where(where) : source;
        }
    }
}
