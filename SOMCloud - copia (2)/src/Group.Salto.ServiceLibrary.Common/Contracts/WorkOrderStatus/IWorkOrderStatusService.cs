using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderStatus;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderStatus
{
    public interface IWorkOrderStatusService
    {
        ResultDto<IList<WorkOrderStatusListDto>> GetAllFilteredByLanguage(WorkOrderStatusFilterDto filter);
        ResultDto<WorkOrderStatusListDto> GetById(int id);
        ResultDto<WorkOrderStatusListDto> Create(WorkOrderStatusListDto model);
        ResultDto<WorkOrderStatusListDto> Update(WorkOrderStatusListDto model);
        ResultDto<bool> Delete(int id);
        ResultDto<bool> CanDelete(int id);
        IList<BaseNameIdDto<int>> GetAllKeyValues(int languageId);
        ResultDto<List<MultiSelectItemDto>> GetWorkOrderStatusMultiSelect(int languageId, List<int> selectItems);
        string GetNamesComaSeparated(int languageId, List<int> ids);
    }
}