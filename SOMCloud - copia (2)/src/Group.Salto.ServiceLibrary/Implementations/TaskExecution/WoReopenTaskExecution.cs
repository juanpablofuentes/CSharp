using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.ServiceLibrary.Common.Contracts.TaskExecution;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Tasks;

namespace Group.Salto.ServiceLibrary.Implementations.TaskExecution
{
    public class WoReopenTaskExecution : IWoReopenTaskExecution
    {
        private readonly IWorkOrderAnalysisRepository _workOrderAnalysisRepository;

        public WoReopenTaskExecution(IWorkOrderAnalysisRepository workOrderAnalysisRepository)
        {
            _workOrderAnalysisRepository = workOrderAnalysisRepository;
        }

        public ResultDto<bool> PerformTask(TaskExecutionValues taskExecutionValues)
        {
            taskExecutionValues.CurrentWorkOrder.ClosingOtdate = null;
            taskExecutionValues.CurrentWorkOrder.ClientClosingDate = null;
            taskExecutionValues.CurrentWorkOrder.SystemDateWhenOtclosed = null;
            taskExecutionValues.CurrentWorkOrder.MeetSlaresolution = null;
            taskExecutionValues.CurrentWorkOrder.MeetSlaresponse = null;

            if (taskExecutionValues.CurrentWorkOrder.WorkOrderAnalysis != null)
            {
                _workOrderAnalysisRepository.DeleteWorkOrderAnalysisWithoutSaving(taskExecutionValues.CurrentWorkOrder.WorkOrderAnalysis);
            }
            
            return taskExecutionValues.Result;
        }
    }
}
