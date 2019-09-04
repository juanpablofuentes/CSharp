using System;
using System.Collections.Generic;
using Group.Salto.Common;
using Group.Salto.ServiceLibrary.Common.Contracts.Analysis;
using Group.Salto.ServiceLibrary.Common.Contracts.TaskExecution;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Tasks;

namespace Group.Salto.ServiceLibrary.Implementations.TaskExecution
{
    public class AnalysisTaskExecution : IAnalysisTaskExecution
    {
        private readonly IAnalysisService _analysisService;
        private readonly IBillAnalysisService _billAnalysisService;
        private readonly IWoAnalysisService _woAnalysisService;

        public AnalysisTaskExecution(IAnalysisService analysisService,
                                    IBillAnalysisService billAnalysisService,
                                    IWoAnalysisService woAnalysisService)
        {
            _analysisService = analysisService;
            _billAnalysisService = billAnalysisService;
            _woAnalysisService = woAnalysisService;
        }

        public ResultDto<bool> PerformTask(TaskExecutionValues taskExecutionValues)
        {
            try
            {
                _analysisService.AddOrUpdateServiceAnalysis(taskExecutionValues.CurrentWorkOrder, taskExecutionValues.CreatedService, taskExecutionValues.CurrentPeople);
                taskExecutionValues.Result = _billAnalysisService.AddAllBillsToAnalize(taskExecutionValues.CurrentWorkOrder);
                if (taskExecutionValues.Result.Errors == null)
                {
                    var closingDate = _analysisService.GetServiceClosingDate(taskExecutionValues.CreatedService);
                    if (closingDate.HasValue)
                    {
                        taskExecutionValues.CurrentWorkOrder.ClientClosingDate = taskExecutionValues.CurrentWorkOrder.ClientClosingDate ?? closingDate;
                        taskExecutionValues.CurrentWorkOrder.ClosingOtdate = closingDate;
                        taskExecutionValues.CurrentWorkOrder.SystemDateWhenOtclosed = DateTime.UtcNow;
                        if (taskExecutionValues.CurrentWorkOrder.WorkOrderAnalysis == null)
                        {
                            taskExecutionValues.Result = _woAnalysisService.AddWoAnalysis(taskExecutionValues.CurrentWorkOrder);
                        }
                        else
                        {
                            taskExecutionValues.Result = _woAnalysisService.UpdateWoAnalysis(taskExecutionValues.CurrentWorkOrder);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                taskExecutionValues.Result.Data = false;
                taskExecutionValues.Result.Errors = new ErrorsDto { Errors = new List<ErrorDto> { new ErrorDto { ErrorType = ErrorType.TaskExecutionProcessError, ErrorMessageKey = e.ToString() } } };
            }
            
            return taskExecutionValues.Result;
        }
    }
}