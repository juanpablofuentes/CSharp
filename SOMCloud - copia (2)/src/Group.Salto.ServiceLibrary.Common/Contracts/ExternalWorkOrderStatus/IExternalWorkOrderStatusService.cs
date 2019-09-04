using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.ExternalWorkOrderStatus;

namespace Group.Salto.ServiceLibrary.Common.Contracts.ExternalWorkOrderStatus
{
    public interface IExternalWorkOrderStatusService
    {
        ResultDto<IList<ExternalWorkOrderStatusListDto>> GetAllFilteredByLanguage(ExternalWorkOrderStatusFilterDto filter);
        ResultDto<ExternalWorkOrderStatusListDto> GetById(int id);
        ResultDto<ExternalWorkOrderStatusListDto> Create(ExternalWorkOrderStatusListDto model);
        ResultDto<ExternalWorkOrderStatusListDto> Update(ExternalWorkOrderStatusListDto model);
        ResultDto<bool> Delete(int id);
        ResultDto<bool> CanDelete(int id);
        IList<BaseNameIdDto<int>> GetAllKeyValues(int languageId);
        ResultDto<List<MultiSelectItemDto>> GetExternalWorkOrderStatusMultiSelect(int languageId, List<int> selectItems);
        string GetNamesComaSeparated(int languageId, List<int> ids);
    }
}