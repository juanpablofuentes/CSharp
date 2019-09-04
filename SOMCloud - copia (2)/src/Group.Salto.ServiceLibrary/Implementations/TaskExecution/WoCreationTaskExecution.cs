using Group.Salto.ServiceLibrary.Common.Contracts.TaskExecution;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Tasks;

namespace Group.Salto.ServiceLibrary.Implementations.TaskExecution
{
    public class WoCreationTaskExecution : IWoCreationTaskExecution
    {
        public ResultDto<bool> PerformTask(TaskExecutionValues taskExecutionValues)
        {
            //Not used
            return taskExecutionValues.Result;
        }
    }
}
