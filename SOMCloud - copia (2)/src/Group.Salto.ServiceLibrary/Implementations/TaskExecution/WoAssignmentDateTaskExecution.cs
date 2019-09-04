using Group.Salto.ServiceLibrary.Common.Contracts.TaskExecution;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Tasks;

namespace Group.Salto.ServiceLibrary.Implementations.TaskExecution
{
    public class WoAssignmentDateTaskExecution : IWoAssignmentDateTaskExecution
    {
        public ResultDto<bool> PerformTask(TaskExecutionValues taskExecutionValues)
        {
            taskExecutionValues.CurrentWorkOrder.AssignmentTime = taskExecutionValues.TaskParameters.Date;
            return taskExecutionValues.Result;
        }
    }
}
