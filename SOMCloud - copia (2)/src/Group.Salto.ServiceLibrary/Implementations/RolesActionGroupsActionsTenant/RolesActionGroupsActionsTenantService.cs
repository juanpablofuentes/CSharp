using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.RolesActionGroupsActionsTenant;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.RolesActionGroupsActionsTenant;
using System;
using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Extensions;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.RolesActionGroupsActionsTenant
{
    public class RolesActionGroupsActionsTenantService : BaseService, IRolesActionGroupsActionsTenantService
    {
        private IRolesActionGroupsActionsTenantRepository _rolesActionGroupsActionsTenant;

        public RolesActionGroupsActionsTenantService(ILoggingService logginingService,
            IRolesActionGroupsActionsTenantRepository rolesActionGroupsActionsTenant) : base(logginingService)
        {
            _rolesActionGroupsActionsTenant = rolesActionGroupsActionsTenant ?? throw new ArgumentNullException(nameof(IRolesActionGroupsActionsTenantRepository));
        }

        public ResultDto<IList<RoleActionGroupActionTenantDto>> GetRolesActionsByRolId(string rolId)
        {
            LogginingService.LogInfo($"Get roles actions by rol id");
            var data = _rolesActionGroupsActionsTenant.GetRolesActionsByRolId(rolId);
            return ProcessResult(data.ToList().ToDto());
        }
    }
}