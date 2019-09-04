using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Tasks;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Tasks
{
    public interface ITaskExecution
    {
        ResultDto<bool> PerformTask(TaskExecutionValues taskExecutionValues);
    }
}
