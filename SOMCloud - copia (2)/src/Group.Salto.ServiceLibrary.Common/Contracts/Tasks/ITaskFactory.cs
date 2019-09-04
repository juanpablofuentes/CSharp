using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Enums;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Tasks
{
    public interface ITaskFactory
    {
        ITaskExecution GetTaskExecution(TaskActionTypeEnum taskType);
    }
}
