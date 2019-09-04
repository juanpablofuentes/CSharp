using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.LiteralPreconditions;
using Group.Salto.ServiceLibrary.Common.Dtos.Tasks;
using Group.Salto.ServiceLibrary.Common.Dtos.Trigger;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Trigger
{
    public interface ITriggerService
    {
        TriggerDto GetTriggerByTaskId(int id);
        IList<BaseNameIdDto<int>> GetTriggerValues(string triggerTypeName, FilterQueryParametersBase filterQueryParameters);
        ResultDto<TasksDto> Update(TriggerDto model);
    }
}