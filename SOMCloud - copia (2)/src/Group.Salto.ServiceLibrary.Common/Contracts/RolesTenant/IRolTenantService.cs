using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.RolesTenant;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.RolesTenant
{
    public interface IRolTenantService
    {
        ResultDto<IEnumerable<RolTenantDto>> GetAll();
        ResultDto<RolTenantDto> GetById(int Id);
        ResultDto<RolTenantDto> Create(RolTenantDto rolTenant);
        ResultDto<RolTenantDto> Update(RolTenantDto rolTenant);
        ResultDto<IList<RolTenantListDto>> GetListFiltered(RolTenantFilterDto filter);
    }
}