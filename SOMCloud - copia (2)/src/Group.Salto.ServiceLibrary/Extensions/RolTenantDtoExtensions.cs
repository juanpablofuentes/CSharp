using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.RolesTenant;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class RolTenantDtoExtensions
    {
        public static RolesTenant ToEntity(this RolTenantDto source)
        {
            RolesTenant result = null;
            if (source != null)
            {
                result = new RolesTenant()
                {
                    Id = source.Id.ToString(),
                    Name = source.Name,
                    Description = source.Description
                };
            }
            return result;
        }

        public static RolTenantDto ToDto(this RolesTenant source)
        {
            RolTenantDto result = null;
            if (source != null)
            {
                result = new RolTenantDto()
                {
                    Id = System.Convert.ToInt32(source.Id),
                    Name = source.Name,
                    Description = source.Description
                };
            }
            return result;
        }

        public static IList<RolTenantDto> ToDtoList(this IList<RolesTenant> source)
        {
            return source.MapList(x => x.ToDto());
        }

        public static void UpdateRolTenantEntity(this RolesTenant target, RolesTenant source)
        {
            if (source != null && target != null)
            {
                target.Name = source.Name;
                target.Description = source.Description;
            }
        }
    }
}