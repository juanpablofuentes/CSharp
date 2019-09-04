using Group.Salto.ServiceLibrary.Common.Contracts.TaskExecution;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Tasks;

namespace Group.Salto.ServiceLibrary.Implementations.TaskExecution
{
    public class WoClosingDateTaskExecution : IWoClosingDateTaskExecution
    {
        public ResultDto<bool> PerformTask(TaskExecutionValues taskExecutionValues)
        {
            taskExecutionValues.CurrentWorkOrder.ClosingOtdate = taskExecutionValues.TaskParameters.Date;
            taskExecutionValues.CurrentWorkOrder.ClientClosingDate = taskExecutionValues.TaskParameters.Date;
            taskExecutionValues.CurrentWorkOrder.SystemDateWhenOtclosed = taskExecutionValues.TaskParameters.Date;
            return taskExecutionValues.Result;
        }
    }
}
