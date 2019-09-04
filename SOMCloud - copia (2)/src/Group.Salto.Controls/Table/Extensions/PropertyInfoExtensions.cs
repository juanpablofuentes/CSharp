using System.Reflection;

namespace Group.Salto.Controls.Table.Extensions
{
    static class PropertyInfoExtensions
    {
        internal static string GetSafeStringValue(this PropertyInfo property, object source)
        {
            if (!property.CanRead) return string.Empty;
            var value = property.GetValue(source);
            return value != null ? value.ToString() : string.Empty;
        }
    }
}
