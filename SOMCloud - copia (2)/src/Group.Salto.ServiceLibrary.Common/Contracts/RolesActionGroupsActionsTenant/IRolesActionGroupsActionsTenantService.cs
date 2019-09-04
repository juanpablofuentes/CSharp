using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.RolesActionGroupsActionsTenant;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.RolesActionGroupsActionsTenant
{
    public interface IRolesActionGroupsActionsTenantService
    {
        ResultDto<IList<RoleActionGroupActionTenantDto>> GetRolesActionsByRolId(string rolId);
    }
}