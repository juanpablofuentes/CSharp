using Group.Salto.ServiceLibrary.Common.Contracts.Analysis;
using Group.Salto.ServiceLibrary.Common.Contracts.TaskExecution;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Tasks;

namespace Group.Salto.ServiceLibrary.Implementations.TaskExecution
{
    public class WoAccountingClosingDateExecution : IWoAccountingClosingDateExecution
    {
        private readonly IAnalysisService _analysisService;
        private readonly IWoAnalysisService _woAnalysisService;
        private readonly IBillAnalysisService _billAnalysisService;

        public WoAccountingClosingDateExecution(IAnalysisService analysisService,
                                                IWoAnalysisService woAnalysisService,
                                                IBillAnalysisService billAnalysisService)
        {
            _analysisService = analysisService;
            _woAnalysisService = woAnalysisService;
            _billAnalysisService = billAnalysisService;
        }

        public ResultDto<bool> PerformTask(TaskExecutionValues taskExecutionValues)
        {
            taskExecutionValues.CurrentWorkOrder.AccountingClosingDate = taskExecutionValues.TaskParameters.Date;
            taskExecutionValues.Result = _analysisService.AddAllServicesToAnalize(taskExecutionValues.CurrentWorkOrder, taskExecutionValues.CurrentPeople);
            if (taskExecutionValues.Result.Errors != null && taskExecutionValues.CurrentWorkOrder.WorkOrderAnalysis != null)
            {
                taskExecutionValues.Result = _woAnalysisService.UpdateWoAnalysis(taskExecutionValues.CurrentWorkOrder);
                if (taskExecutionValues.Result.Errors != null)
                {
                    taskExecutionValues.Result = _billAnalysisService.AddAllBillsToAnalize(taskExecutionValues.CurrentWorkOrder);
                }
            }
            return taskExecutionValues.Result;
        }
    }
}
