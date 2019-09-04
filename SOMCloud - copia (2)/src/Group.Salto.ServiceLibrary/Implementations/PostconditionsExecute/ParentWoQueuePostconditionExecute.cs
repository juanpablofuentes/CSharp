using System;
using Group.Salto.ServiceLibrary.Common.Contracts.PostconditionExecute;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Postconditions;

namespace Group.Salto.ServiceLibrary.Implementations.PostconditionsExecute
{
    public class ParentWoQueuePostconditionExecute : IParentWoQueuePostconditionExecute
    {
        public ResultDto<bool> PerformPostcondition(PostconditionExecutionValues postconditionExecutionValues)
        {
            if (postconditionExecutionValues.WorkOrder.WorkOrdersFather != null && postconditionExecutionValues.Postcondition.QueueId.HasValue)
            {
                postconditionExecutionValues.WorkOrder.WorkOrdersFather.QueueId = postconditionExecutionValues.Postcondition.QueueId.Value;
            }

            return postconditionExecutionValues.Result;
        }
    }
}
