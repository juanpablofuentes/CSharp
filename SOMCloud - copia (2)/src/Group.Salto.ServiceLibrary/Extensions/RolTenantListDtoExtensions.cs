using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.RolesTenant;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class RolTenantListDtoExtensions
    {
        public static RolTenantListDto ToListDto(this RolesTenant source)
        {
            RolTenantListDto result = null;
            if (source != null)
            {
                result = new RolTenantListDto()
                {
                    Id = Convert.ToInt32(source.Id),
                    Name = source.Name,
                    Description = source.Description
                };
            }
            return result;
        }

        public static IList<RolTenantListDto> ToListDto(this IList<RolesTenant> source)
        {
            return source?.MapList(c => c.ToListDto());
        }
    }
}