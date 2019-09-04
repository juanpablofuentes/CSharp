using System;
using Group.Salto.ServiceLibrary.Common.Contracts.PostconditionExecute;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Postconditions;

namespace Group.Salto.ServiceLibrary.Implementations.PostconditionsExecute
{
    public class QueuePostconditionExecution : IQueuePostconditionExecution
    {
        public ResultDto<bool> PerformPostcondition(PostconditionExecutionValues postconditionExecutionValues)
        {
            postconditionExecutionValues.WorkOrder.QueueId = postconditionExecutionValues.Postcondition.QueueId ?? int.MinValue;
            return postconditionExecutionValues.Result;
        }
    }
}
