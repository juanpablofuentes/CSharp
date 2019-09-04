using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Contracts.Derivative;
using Group.Salto.ServiceLibrary.Common.Contracts.TaskExecution;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Tasks;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.TaskExecution
{
    public class WoDerivedServicesTaskExecution : IWoDerivedServicesTaskExecution
    {
        private readonly IConfiguration _configuration;
        private readonly IDerivedCloneService _derivedCloneService;

        public WoDerivedServicesTaskExecution(IConfiguration configuration,
                                              IDerivedCloneService derivedCloneService)
        {
            _configuration = configuration;
            _derivedCloneService = derivedCloneService;
        }

        public ResultDto<bool> PerformTask(TaskExecutionValues taskExecutionValues)
        {
            var serviceFolder = _configuration.GetSection(AppsettingsKeys.BlobStorageFolders).GetValue<string>(AppsettingsKeys.BlobStorageFolderServices);
            var container = _configuration.GetSection(AppsettingsKeys.AzureBlobStorage).GetValue<string>(AppsettingsKeys.StorageName);
            var dServices = taskExecutionValues.CurrentTask.DerivedServices.Where(x => x.ProjectId == taskExecutionValues.CurrentWorkOrder.ProjectId);
            foreach (var derivedService in dServices)
            {
                var result = _derivedCloneService.CreateService(taskExecutionValues.CurrentPeople, serviceFolder, container, derivedService, taskExecutionValues.TaskParameters.ResponsibleId);
                if (result?.Data != null && !result.Errors.Errors.Any())
                {
                    taskExecutionValues.CurrentWorkOrder.Services.Add(result.Data);
                }
                else
                {
                    taskExecutionValues.Result.Data = false;
                    taskExecutionValues.Result.Errors = result.Errors;
                    break;
                }
            }

            return taskExecutionValues.Result;
        }
    }
}
