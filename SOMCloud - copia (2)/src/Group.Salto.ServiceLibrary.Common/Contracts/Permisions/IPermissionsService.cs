using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Permisions;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Permisions
{
    public interface IPermissionsService
    {
        IEnumerable<PermissionsDto> GetAllKeyValues();
        ResultDto<IList<PermissionsDto>> GetAllFiltered(PermissionsFilterDto filter);
        ResultDto<PermissionDetailDto> GetById(int id);
        ResultDto<PermissionDetailDto> Create(PermissionDetailDto model);
        ResultDto<PermissionDetailDto> Update(PermissionDetailDto model);
        ResultDto<bool> Delete(int id);
        ResultDto<List<MultiSelectItemDto>> GetPermissionList();
    }
}