using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using Group.Salto.ServiceLibrary.Common.Dtos.SiteUser;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.SiteUser
{
    public interface ISiteUserService
    {
        ResultDto<IList<SiteUserListDto>> GetAllFiltered(SiteUserFilterDto filter);
        IList<BaseNameIdDto<int>> FilterByClientSite(QueryCascadeDto queryCascadeDto);
        ResultDto<SiteUserDetailDto> GetById(int id);
        ResultDto<SiteUserDetailDto> Update(SiteUserDetailDto model);
        ResultDto<SiteUserDetailDto> Create(SiteUserDetailDto model);
        ResultDto<bool> Delete(int id);
        ResultDto<ErrorDto> CanDelete(int id);
    }
}