using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderDerivated;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderDerivated
{
    public interface IWorkOrderDerivatedService
    {
        ResultDto<WorkOrderDerivatedDto> GetById(int Id);
        ResultDto<WorkOrderDerivatedDto> Create(WorkOrderDerivatedDto workOrderEditDto);
        ResultDto<WorkOrderDerivatedDto> Update(WorkOrderDerivatedDto workOrderEditDto);
        IList<BaseNameIdDto<int>> GetDuplicationPolicyItemsKeyValues(int languageId);
        ResultDto<List<List<BaseNameIdDto<string>>>> GetByTaskId(int Id, int languageId);
    }
}