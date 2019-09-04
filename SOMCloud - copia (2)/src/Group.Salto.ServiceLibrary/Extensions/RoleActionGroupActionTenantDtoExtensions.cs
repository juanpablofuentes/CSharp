using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.RolesActionGroupsActionsTenant;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class RoleActionGroupActionTenantDtoExtensions
    {
        public static RoleActionGroupActionTenantDto ToDto(this RolesActionGroupsActionsTenant source)
        {
            RoleActionGroupActionTenantDto result = null;
            if (source != null)
            {
                result = new RoleActionGroupActionTenantDto()
                {
                    RoleTenantId = source.RoleTenantId,
                    ActionGroupId = source.ActionGroupId,
                    ActionId = source.ActionId
                };
            }
            return result;
        }

        public static IList<RoleActionGroupActionTenantDto> ToDto(this IList<RolesActionGroupsActionsTenant> source)
        {
            return source?.MapList(c => c.ToDto());
        }
    }
}