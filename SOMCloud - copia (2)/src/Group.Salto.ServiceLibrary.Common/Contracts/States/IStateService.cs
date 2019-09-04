using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.States
{
    public interface IStateService
    {
        IList<BaseNameIdDto<int>> Filter(QueryRequestDto queryRequest);
        string GetStatesForInsert();
        IList<BaseNameIdDto<int>> GetAllKeyValues();
        ResultDto<List<MultiSelectItemDto>> GetStatesMultiSelect(List<int> selectItems);
        string GetNamesComaSeparated(List<int> ids);
    }
}