using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using Group.Salto.ServiceLibrary.Common.Dtos.Sites;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Sites
{
    public interface ISitesService
    {
        ResultDto<IList<SitesListDto>> GetAllFiltered(SitesFilterDto filter);
        IList<BaseNameIdDto<int>> Filter(QueryRequestDto queryRequest);
        ResultDto<SitesDetailDto> GetById(int id);
        ResultDto<SitesDetailDto> Create(SitesDetailDto model);
        ResultDto<SitesDetailDto> Update(SitesDetailDto model);
        IList<BaseNameIdDto<int>> FilterByClientSite(QueryCascadeDto queryCascadeDto);
        ResultDto<bool> Delete(int id);
        ResultDto<ErrorDto> CanDelete(int id);
        ResultDto<IList<Dtos.AdvancedSearch.AdvancedSearchDto>> GetAdvancedSearch(AdvancedSearchQueryTypeDto queryTypeParameters);
    }
}