using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.FinalClients;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.FinalClients
{
    public interface IFinalClientsServices
    {
        ResultDto<FinalClientsDetailDto> GetById(int id);
        ResultDto<FinalClientsDetailDto> Update(FinalClientsDetailDto finalClient);
        ResultDto<FinalClientsDetailDto> Create(FinalClientsDetailDto finalClient);
        ResultDto<bool> Delete(int id);
        ResultDto<IList<FinalClientsListDto>> GetAllFiltered(FinalClientsFilterDto filter);
        IList<BaseNameIdDto<int>> Filter(QueryRequestDto queryRequest);
        ResultDto<ErrorDto> CanDelete(int id);
        ResultDto<IList<Dtos.AdvancedSearch.AdvancedSearchDto>> GetAdvancedSearch(AdvancedSearchQueryTypeDto queryTypeParameters);
        IList<BaseNameIdDto<int>> GetAllKeyValues();
        ResultDto<List<MultiSelectItemDto>> GetFinalClientMultiSelect(List<int> selectItems);
        string GetNamesComaSeparated(List<int> ids);
    }
}