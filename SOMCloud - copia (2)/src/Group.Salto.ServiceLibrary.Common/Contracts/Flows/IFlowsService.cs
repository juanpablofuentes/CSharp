using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Flows;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Flows
{
    public interface IFlowsService
    {
        ResultDto<IList<FlowsListDto>> GetAllFiltered(FlowsFilterDto filter);
        ResultDto<FlowsDto> GetById(int id);
        ResultDto<FlowsDto> Update(FlowsDto model);
        ResultDto<FlowsDto> Create(FlowsDto model);
        ResultDto<FlowsWithTasksDictionaryDto> GetFlowsWithTasksInfoById(int id, int languageId);
    }
}