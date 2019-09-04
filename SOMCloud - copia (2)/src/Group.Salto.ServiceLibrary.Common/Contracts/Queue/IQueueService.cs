using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Queue;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderViewConfigurations;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Queue
{
    public interface IQueueService
    {
        ResultDto<IList<QueueListDto>> GetAllFilteredByLanguage(QueueFilterDto filter);
        ResultDto<QueueListDto> GetById(int id);
        ResultDto<QueueListDto> Create(QueueListDto model);
        ResultDto<QueueListDto> Update(QueueListDto model);
        ResultDto<bool> Delete(int id);
        ResultDto<bool> CanDelete(int id);
        IList<BaseNameIdDto<int>> GetAllKeyValues(int languageId);
        IList<BaseNameIdDto<int>> GetAllKeyValuesWithPermissions(int languageId, int userId);
        ResultDto<List<MultiSelectItemDto>> GetQueueMultiSelect(MultiSelectConfigurationViewDto multiSelectConfigurationViewDto, List<int> selectItems);
        string GetNamesComaSeparated(int languageId, List<int> ids);
    }
}