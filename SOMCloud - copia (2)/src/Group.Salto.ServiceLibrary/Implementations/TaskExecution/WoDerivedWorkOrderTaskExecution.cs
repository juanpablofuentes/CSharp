using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common;
using Group.Salto.Common.Constants.WorkOrder;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.ServiceLibrary.Common.Contracts.Derivative;
using Group.Salto.ServiceLibrary.Common.Contracts.TaskExecution;
using Group.Salto.ServiceLibrary.Common.Contracts.Tasks;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Enums;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Tasks;
using Group.Salto.ServiceLibrary.Extensions;
using Microsoft.Extensions.Configuration;

namespace Group.Salto.ServiceLibrary.Implementations.TaskExecution
{
    public class WoDerivedWorkOrderTaskExecution : IWoDerivedWorkOrderTaskExecution
    {
        private readonly IConfiguration _configuration;
        private readonly IDerivedCloneService _derivedCloneService;
        private readonly ITaskRepository _taskRepository;
        private readonly ITasksService _tasksService;

        public WoDerivedWorkOrderTaskExecution(IConfiguration configuration,
                                               IDerivedCloneService derivedCloneService,
                                               ITaskRepository taskRepository,
                                               ITasksService tasksService)
        {
            _configuration = configuration;
            _derivedCloneService = derivedCloneService;
            _taskRepository = taskRepository;
            _tasksService = tasksService;
        }

        public ResultDto<bool> PerformTask(TaskExecutionValues taskExecutionValues)
        {
            var serviceFolder = _configuration.GetSection(AppsettingsKeys.BlobStorageFolders).GetValue<string>(AppsettingsKeys.BlobStorageFolderServices);
            var container = _configuration.GetSection(AppsettingsKeys.AzureBlobStorage).GetValue<string>(AppsettingsKeys.StorageName);

            foreach (var derivedWo in taskExecutionValues.CurrentTask.WorkOrdersDeritative)
            {
                var newWo = _derivedCloneService.CreateWorkOrder(derivedWo, taskExecutionValues.CurrentWorkOrder);
                if (newWo.SiteUserId <= 0)
                {
                    newWo.SiteUserId = int.MinValue;
                }
                if (taskExecutionValues.CreatedService != null)
                {
                    newWo.Service = taskExecutionValues.CreatedService; //add service to analysis
                }

                newWo.PeopleIntroducedById = taskExecutionValues.CurrentPeople.Id;

                if (derivedWo.OtherServicesDuplicationPolicy == (int)ServiceDuplicationPolicyEnum.Clone)
                {
                    foreach (var service in taskExecutionValues.CurrentWorkOrder.Services)
                    {
                        var result = _derivedCloneService.CloneService(taskExecutionValues.CurrentPeople, serviceFolder, container, service, newWo);
                        if (result.Data != null && result.Errors != null && !result.Errors.Errors.Any())
                        {
                            newWo.Services.Add(result.Data);
                        }
                        else
                        {
                            taskExecutionValues.Result.Data = false;
                            taskExecutionValues.Result.Errors = result.Errors;
                            break;
                        }
                    }
                }
                if (!taskExecutionValues.Result.Data)
                {
                    break;
                }
                var refGeneratorService = derivedWo.GeneratorServiceDuplicationPolicy == (int)ServiceDuplicationPolicyEnum.Reference;
                var refOtherServices = derivedWo.OtherServicesDuplicationPolicy == (int)ServiceDuplicationPolicyEnum.Reference;
                taskExecutionValues.Result = DoCreateWorkOrder(newWo, refGeneratorService, refOtherServices, taskExecutionValues.CurrentPeople, taskExecutionValues.Result, taskExecutionValues.CustomerId, taskExecutionValues.TaskParameters.ResponsibleId);
            }

            return taskExecutionValues.Result;
        }

        private ResultDto<bool> DoCreateWorkOrder(WorkOrders newWo, bool refGeneratorService, bool refOtherServices, Entities.Tenant.People currentPeople, ResultDto<bool> result, Guid customerId, int taskParametersResponsibleId)
        {
            try
            {
                if (newWo.WorkOrderStatusId >= 0 && newWo.ExternalWorOrderStatusId != null && newWo.ExternalWorOrderStatusId >= 0 && newWo.PeopleIntroducedById >= 0 && newWo.ProjectId >= 0 && newWo.FinalClientId >= 0 && newWo.QueueId >= 0 && newWo.OriginId >= 0 && currentPeople != null)
                {
                    newWo.CreationDate = newWo.CreationDate != DateTime.MinValue ? newWo.CreationDate : DateTime.UtcNow;
                    newWo.PickUpTime = newWo.PickUpTime != null && newWo.PickUpTime != DateTime.MinValue ? newWo.PickUpTime : DateTime.UtcNow;
                    newWo.AssignmentTime = newWo.AssignmentTime != null && newWo.AssignmentTime != DateTime.MinValue ? newWo.AssignmentTime : DateTime.UtcNow;
                    newWo.ReferenceGeneratorService = refGeneratorService;
                    newWo.ReferenceOtherServices = refOtherServices;
                    newWo.ResolutionDateSla = newWo.ResolutionDateSla != DateTime.MinValue ? newWo.ResolutionDateSla : null;
                    newWo.DateUnansweredPenaltySla = newWo.DateUnansweredPenaltySla != DateTime.MinValue ? newWo.DateUnansweredPenaltySla : null;
                    newWo.DatePenaltyWithoutResolutionSla = newWo.DatePenaltyWithoutResolutionSla != DateTime.MinValue ? newWo.DatePenaltyWithoutResolutionSla : null;
                    newWo.ResponseDateSla = newWo.ResponseDateSla != DateTime.MinValue ? newWo.ResponseDateSla : null;
                    newWo.Audits = new List<Audits>
                    {
                        new Audits
                        {
                            Name = WorkOrderConstants.WorkOrderAuditName,
                            DataHora = DateTime.UtcNow,
                            UserConfigurationId = currentPeople?.UserConfigurationId ?? int.MinValue,
                            UserConfigurationSupplanterId = null,
                            Latitude = null,
                            Longitude = null,
                            Origin = WorkOrderConstants.WorkOrderAuditOrigin,
                            Observations = string.Empty
                        }
                    };
                    newWo.InternalCreationDate = DateTime.UtcNow;
                    var woTypes = newWo.WorkOrderTypes?.ToWorkOrderFatherListDto().Select(t => t.Id);
                    var tasks = _taskRepository.GetAvailableTasksFromWoId(currentPeople, newWo, woTypes).Where(t => t.NameFieldModel == TaskActionTypeEnum.Creacio.ToString()).ToList();
                    foreach (var task in tasks)
                    {
                        try
                        {
                            var taskExecute = new TaskExecuteDto
                            {
                                Id = task.Id,
                                WorkOrderId = newWo.Id,
                                Type = TaskTypeEnum.Creacio,
                                ResponsibleId = taskParametersResponsibleId
                            };
                            result = _tasksService.TaskExecute(taskExecute, currentPeople.UserConfigurationId ?? 0, customerId, newWo);
                        }
                        catch (Exception e)
                        {
                            result.Data = false;
                            if (result.Errors == null)
                            {
                                result.Errors = new ErrorsDto();
                            }
                            result.Errors.AddError(new ErrorDto
                            {
                                ErrorMessageKey = e.ToString(),
                                ErrorType = ErrorType.ValidationError
                            });
                        }
                        if (!result.Data)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                result.Data = false;
                if (result.Errors == null)
                {
                    result.Errors = new ErrorsDto();
                }
                result.Errors.AddError(new ErrorDto
                {
                    ErrorMessageKey = e.ToString(),
                    ErrorType = ErrorType.ValidationError
                });
            }
            return result;
        }
    }
}
