using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class PredefinedServicesPermissionDtoExtensions
    {
        public static string ToPredefinedServicesString(this IList<PredefinedServicesPermission> source)
        {
            StringBuilder result = new StringBuilder();
            if (source != null)
            {
                foreach (PredefinedServicesPermission predefinedServicesPermission in source)
                {
                    result.Append($"{predefinedServicesPermission.Permission?.Name}, ");
                }
            }

            return (result.Length > 2) ? result.Remove(result.Length - 2, 2).ToString() : result.ToString();
        }

        public static List<int> ToPredefinedServicesIds(this IList<PredefinedServicesPermission> source)
        {
            List<int> result = new List<int>();
            if (source != null)
            {
                foreach (PredefinedServicesPermission predefinedServicesPermission in source)
                {
                    result.Add(predefinedServicesPermission.PermissionId);
                }
            }
            return result;
        }
    }
}