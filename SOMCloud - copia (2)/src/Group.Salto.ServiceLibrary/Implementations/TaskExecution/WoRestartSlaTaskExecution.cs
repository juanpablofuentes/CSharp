using Group.Salto.ServiceLibrary.Common.Contracts.TaskExecution;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Tasks;
using System;

namespace Group.Salto.ServiceLibrary.Implementations.TaskExecution
{
    public class WoRestartSlaTaskExecution : IWoRestartSlaTaskExecution
    {
        public ResultDto<bool> PerformTask(TaskExecutionValues taskExecutionValues)
        {
            if (taskExecutionValues.CurrentWorkOrder.DateStopTimerSla != null && taskExecutionValues.CurrentWorkOrder.DateStopTimerSla != DateTime.MinValue)
            {
                var delta = (DateTime.UtcNow - (taskExecutionValues.CurrentWorkOrder.DateStopTimerSla ?? DateTime.UtcNow)).TotalSeconds;
                if (taskExecutionValues.CurrentWorkOrder.ResponseDateSla != DateTime.MinValue)
                {
                    taskExecutionValues.CurrentWorkOrder.ResponseDateSla = (taskExecutionValues.CurrentWorkOrder.ResponseDateSla ?? DateTime.UtcNow).AddSeconds(delta);
                }
                if (taskExecutionValues.CurrentWorkOrder.ResolutionDateSla != DateTime.MinValue)
                {
                    taskExecutionValues.CurrentWorkOrder.ResolutionDateSla = (taskExecutionValues.CurrentWorkOrder.ResolutionDateSla ?? DateTime.UtcNow).AddSeconds(delta);
                }
                if (taskExecutionValues.CurrentWorkOrder.DateUnansweredPenaltySla != DateTime.MinValue)
                {
                    taskExecutionValues.CurrentWorkOrder.DateUnansweredPenaltySla = (taskExecutionValues.CurrentWorkOrder.DateUnansweredPenaltySla ?? DateTime.UtcNow).AddSeconds(delta);
                }
                if (taskExecutionValues.CurrentWorkOrder.DatePenaltyWithoutResolutionSla != DateTime.MinValue)
                {
                    taskExecutionValues.CurrentWorkOrder.DatePenaltyWithoutResolutionSla = (taskExecutionValues.CurrentWorkOrder.DatePenaltyWithoutResolutionSla ?? DateTime.UtcNow).AddSeconds(delta);
                }
                taskExecutionValues.CurrentWorkOrder.DateStopTimerSla = null;
            }
            return taskExecutionValues.Result;
        }
    }
}
