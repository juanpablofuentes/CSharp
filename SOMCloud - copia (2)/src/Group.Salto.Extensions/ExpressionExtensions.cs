using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Group.Salto.Extensions
{
    public static class ExpressionExtensions<T>
    {
        public static PropertyInfo AsPropertyInfo(Expression<Func<T, dynamic>> prop)
        {
            var memberExp = prop.Body.GetType() == typeof(UnaryExpression)
               ? ((UnaryExpression)prop.Body).Operand as MemberExpression
               : prop.Body as MemberExpression;

            return memberExp.Member as PropertyInfo;
        }

        public static string AsPropertyName(Expression<Func<T, dynamic>> prop)
        {
            var propertyInfo = AsPropertyInfo(prop);
            return propertyInfo != null ? propertyInfo.Name : null;
        }

        public static PropertyInfo AsPropertyInfo<TR>(Expression<Func<T, TR>> prop)
        {
            var memberExp = prop.Body.GetType() == typeof(UnaryExpression)
               ? ((UnaryExpression)prop.Body).Operand as MemberExpression
               : prop.Body as MemberExpression;
            return memberExp.Member as PropertyInfo;
        }

        public static string AsPropertyName<TR>(Expression<Func<T, TR>> prop)
        {
            var propertyInfo = AsPropertyInfo(prop);
            return propertyInfo != null ? propertyInfo.Name : null;
        }
    }
}
