using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Permisions;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class PermissionsDtoExtensions
    {
        public static PermissionsDto ToMultiSelectListDto(this KeyValuePair<int, string> source)
        {
            PermissionsDto result = null;
            
            result = new PermissionsDto()
            {
                Id = source.Key,
                Name = source.Value
            };

            return result;
        }

        public static PermissionsDto ToDto(this Permissions source)
        {
            PermissionsDto result = null;
            if (source != null)
            {
                result = new PermissionsDto();
                source.ToDto(result);
            }

            return result;
        }

        public static void ToDto(this Permissions source, PermissionsDto target)
        {
            if (source != null && target != null)
            {
                target.Id = source.Id;
                target.Name = source.Name;
                target.Description = source.Description;
                target.Observations = source.Observations;
            }
        }

        public static IEnumerable<PermissionsDto> ToMultiSelectListDto(this Dictionary<int, string> source)
        {
            foreach(KeyValuePair<int, string> data in source)
            {
               yield return ToMultiSelectListDto(data);
            }
        }
        
        public static IList<PermissionsDto> ToListDto(this IList<Permissions> source)
        {
            return source?.MapList(c => c.ToDto());
        }
    }
}
