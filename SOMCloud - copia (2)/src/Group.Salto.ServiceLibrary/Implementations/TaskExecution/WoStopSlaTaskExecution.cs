using Group.Salto.ServiceLibrary.Common.Contracts.TaskExecution;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Tasks;
using System;

namespace Group.Salto.ServiceLibrary.Implementations.TaskExecution
{
    public class WoStopSlaTaskExecution : IWoStopSlaTaskExecution
    {
        public ResultDto<bool> PerformTask(TaskExecutionValues taskExecutionValues)
        {
            taskExecutionValues.CurrentWorkOrder.DateStopTimerSla = DateTime.UtcNow;
            return taskExecutionValues.Result;
        }
    }
}
