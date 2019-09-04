using Group.Salto.Common;
using Group.Salto.Entities;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.ErpSystemInstanceQuery;
using Group.Salto.ServiceLibrary.Common.Contracts.Postcondition;
using Group.Salto.ServiceLibrary.Common.Contracts.Tasks;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Tasks;
using Group.Salto.ServiceLibrary.Common.Dtos.TasksTranslations;
using Group.Salto.ServiceLibrary.Common.Dtos.Trigger;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Enums;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Postconditions;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Service;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Tasks;
using Group.Salto.ServiceLibrary.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.Tasks
{
    public class TasksService : BaseService, ITasksService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IPermissionsRepository _permissionsRepository;
        private readonly IPeopleRepository _peopleRepository;
        private readonly IWorkOrdersRepository _workOrdersRepository;
        private readonly ITaskFactory _taskFactory;
        private readonly IPostconditionFactory _postconditionFactory;
        private readonly IErpSystemInstanceQueryService _erpSystemInstanceQueryService;
        private readonly IExtraFieldTypesRepository _extraFieldTypesRepository;
        private readonly UserManager<Users> _userManager;
        private readonly IFlowsRepository _flowsRepository;
        private readonly ITriggerTypesRepository _triggerTypesRepository;
        private readonly IServiceRepository _serviceRepository;

        public TasksService(ILoggingService logginingService,
                            ITaskRepository taskRepository,
                            IPeopleRepository peopleRepository,
                            IWorkOrdersRepository workOrdersRepository,
                            ITaskFactory taskFactory,
                            IPostconditionFactory postconditionFactory,
                            IErpSystemInstanceQueryService erpSystemInstanceQueryService,
                            IExtraFieldTypesRepository extraFieldTypesRepository,
                            IPermissionsRepository permissionsRepository,
                            ITasksTranslationsRepository taskTranslationRepository,
                            IFlowsRepository flowsRepository,
                            ITriggerTypesRepository triggerTypesRepository,
                            IServiceRepository serviceRepository,
                            UserManager<Users> userManager) : base(logginingService)
        {
            _taskRepository = taskRepository ?? throw new ArgumentNullException($"{nameof(ITaskRepository)} is null");
            _permissionsRepository = permissionsRepository ?? throw new ArgumentNullException($"{nameof(IPermissionsRepository)} is null");
            _flowsRepository = flowsRepository ?? throw new ArgumentNullException($"{nameof(IFlowsRepository)} is null");
            _triggerTypesRepository = triggerTypesRepository ?? throw new ArgumentNullException($"{nameof(ITriggerTypesRepository)} is null");
            _peopleRepository = peopleRepository;
            _workOrdersRepository = workOrdersRepository;
            _taskFactory = taskFactory;
            _postconditionFactory = postconditionFactory;
            _erpSystemInstanceQueryService = erpSystemInstanceQueryService;
            _extraFieldTypesRepository = extraFieldTypesRepository;
            _userManager = userManager;
            _serviceRepository = serviceRepository ?? throw new ArgumentNullException($"{nameof(IServiceRepository)} is null");
        }

        public TasksDto GetById(int taskId)
        {
            var result = _taskRepository.GetById(taskId);
            return result.ToDto();
        }

        public TasksDetailDto GetByIdWithTranslations(int taskId)
        {
            var result = _taskRepository.GetByIdWithIncludeTranslations(taskId)?.ToDetailDto();
            return result;
        }

        public ResultDto<TasksDto> Update(TasksDto model)
        {
            LogginingService.LogInfo($"Update Task with id {model.TaskId}");

            ResultDto<TasksDto> result = null;
            var localModel = _taskRepository.GetByIdWithIncludeTranslations(model.TaskId);
            if (localModel != null)
            {
                localModel.Name = model.Name;
                localModel.Description = model.Description;
                localModel = UpdateTranslations(localModel, model.TasksPlainTranslations);
                localModel = AssignPermissions(localModel, model.PermissionsTasksSelected);
                var resultSave = _taskRepository.UpdateTask(localModel);
                result = ProcessResult(resultSave.Entity.ToDto(), resultSave);
            }
            return result ?? new ResultDto<TasksDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = model,
            };
        }

        public ResultDto<TasksDto> UpdateTrigger(TriggerDto model, TriggerTypeDto triggerType)
        {
            ResultDto<TasksDto> result = null;
            var localModel = _taskRepository.GetByIdWithIncludeTranslations(model.TaskId);

            if (localModel != null)
            {
                localModel = model.TriggersToEntity(localModel, triggerType);

                var resultSave = _taskRepository.UpdateTask(localModel);
                result = ProcessResult(resultSave.Entity.ToDto(), resultSave);
            }
            return result ?? new ResultDto<TasksDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = localModel.ToDto(),
            };
        }

        public ResultDto<TasksDto> Create(TasksDto model)
        {
            ResultDto<TasksDto> result = null;
            var triggerTypeDefault = _triggerTypesRepository.GetByName("NoAction");
            if (triggerTypeDefault != null)
            {
                model.TriggerTypesId = triggerTypeDefault.Id;
                model.NameFieldModel = triggerTypeDefault.Name; //TODO:Eliminar cuando no se use este campo.
                var entity = model.ToEntity();
                entity = AssignPermissions(entity, model.PermissionsTasksSelected);
                var resultRepository = _taskRepository.CreateTask(entity);
                result = ProcessResult(resultRepository?.Entity?.ToDto(), resultRepository);
            }
            return result;
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues()
        {
            var data = _taskRepository.GetAll();
            return data.Select(x => new BaseNameIdDto<int>()
            {
                Name = x.Name,
                Id = x.Id,
            }).ToList();
        }

        public IEnumerable<TaskApiDto> GetAvailableTasksFromWoId(int userId, int woId)
        {
            LogginingService.LogInfo($"Get tasks from WO {woId}");
            var people = _peopleRepository.GetByConfigIdIncludePermissions(userId);
            var wo = _workOrdersRepository.GetByIdIncludeLocationAndCategory(woId);
            var woTypes = wo.WorkOrderTypes?.ToWorkOrderFatherListDto().Select(t => t.Id);
            var tasks = _taskRepository.GetAvailableTasksFromWoId(people, wo, woTypes).Where(t => t.NameFieldModel != TaskTypeEnum.Creacio.ToString()).ToList();
            var tasksDto = tasks.ToTaskApiDto();
            return tasksDto;
        }

        public TaskInfoDto GetTaskEditInfo(int peopleConfigId, GetTaskDto taskInfoDto)
        {
            var dto = new TaskInfoDto();
            var people = _peopleRepository.GetByConfigIdIncludePermissions(peopleConfigId);
            var currentTask = _taskRepository.GetByIdIncludeBasicInfo(taskInfoDto.TaskId);
            if (currentTask != null && Enum.TryParse(typeof(TaskTypeEnum), currentTask.NameFieldModel, true, out object type))
            {
                var taskType = (TaskTypeEnum)type;
                switch (taskType)
                {
                    case TaskTypeEnum.EstatOTExtern:
                        AddWoExternalChangedState(dto, taskInfoDto.WorkOrderId, currentTask);
                        break;
                    case TaskTypeEnum.TechnicianAndActuationDate:
                    case TaskTypeEnum.Tecnic:
                        AddTecniciansList(dto, people);
                        break;
                    case TaskTypeEnum.Cua:
                        AddWoQueueState(dto, taskInfoDto.WorkOrderId, currentTask);
                        break;
                    case TaskTypeEnum.IdServeiPredefinit:
                        AddPredefinedServiceData(dto, currentTask, people);
                        break;
                    case TaskTypeEnum.EstatOT:
                        AddWoInternalChangedState(dto, taskInfoDto.WorkOrderId, currentTask);
                        break;
                }
            }
            return dto;
        }

        public ResultDto<bool> TaskExecute(TaskExecuteDto taskExecuteParametersDto, int peopleConfigId, Guid customerId)
        {
            LogginingService.LogInfo($"Executing task: {taskExecuteParametersDto.Id}");

            var resultDto = new ResultDto<bool> { Data = true };

            var wo = _workOrdersRepository.GetByIdIncludingExecuteValues(taskExecuteParametersDto.WorkOrderId);
            var woTypes = wo.WorkOrderTypes?.ToWorkOrderFatherListDto().Select(t => t.Id);
            var people = _peopleRepository.GetByConfigIdIncludePermissions(peopleConfigId);
            var currentTask = _taskRepository.GetByIdIncludeBasicInfo(taskExecuteParametersDto.Id);
            var taskAvailable = _taskRepository.GetAvailableTasksFromWoId(people, wo, woTypes).Where(t => t.NameFieldModel != TaskTypeEnum.Creacio.ToString()).Any(t => t.Id == currentTask.Id);

            if (taskAvailable)
            {
                var taskExecutionValues = new TaskExecutionValues
                {
                    CurrentWorkOrder = wo,
                    CurrentPeople = people,
                    CurrentTask = currentTask,
                    TaskParameters = taskExecuteParametersDto,
                    Result = resultDto,
                    WoTypes = woTypes,
                    CustomerId = customerId
                };

                resultDto = ProcessTaskExecutionFlow(taskExecutionValues);

                if (resultDto.Errors?.Errors == null)
                {
                    var saveResult = _workOrdersRepository.UpdateWorkOrder(taskExecutionValues.CurrentWorkOrder);
                    resultDto = ProcessResult(saveResult.IsOk, saveResult);
                    if (resultDto.Errors?.Errors == null)
                    {
                        resultDto = _taskFactory.GetTaskExecution(TaskActionTypeEnum.NotifySubscribers).PerformTask(taskExecutionValues);
                    }
                }
            }
            else
            {
                resultDto.Data = false;
                resultDto.Errors = new ErrorsDto
                {
                    Errors = new List<ErrorDto>
                    {
                        new ErrorDto
                        {
                            ErrorType = ErrorType.ValidationError,
                            ErrorMessageKey = "Task can't execute, permission denied"
                        }
                    }
                };
            }

            if (resultDto.Errors != null)
            {
                foreach (var error in resultDto.Errors.Errors)
                {
                    LogginingService.LogError($"ERROR executing task {taskExecuteParametersDto.Id}: {error.ErrorType} - {error.ErrorMessageKey}");
                }
            }

            return resultDto;
        }

        public ResultDto<bool> TaskExecute(TaskExecuteDto taskExecuteParametersDto, int peopleConfigId, Guid customerId, WorkOrders workOrder)
        {
            var resultDto = new ResultDto<bool> { Data = true };
            var woTypes = workOrder.WorkOrderTypes?.ToWorkOrderFatherListDto().Select(t => t.Id);
            var people = _peopleRepository.GetByConfigIdIncludePermissions(peopleConfigId);
            var currentTask = _taskRepository.GetByIdIncludeBasicInfo(taskExecuteParametersDto.Id);

            var taskExecutionValues = new TaskExecutionValues
            {
                CurrentWorkOrder = workOrder,
                CurrentPeople = people,
                CurrentTask = currentTask,
                TaskParameters = taskExecuteParametersDto,
                Result = resultDto,
                WoTypes = woTypes,
                CustomerId = customerId
            };

            resultDto = ProcessTaskExecutionFlow(taskExecutionValues);

            if (resultDto.Errors?.Errors == null)
            {
                var saveResult = _workOrdersRepository.CreateWorkOrders(taskExecutionValues.CurrentWorkOrder);
                workOrder = saveResult.Entity;
                resultDto = ProcessResult(saveResult.IsOk, saveResult);
                if (resultDto.Errors?.Errors == null)
                {
                    resultDto = _taskFactory.GetTaskExecution(TaskActionTypeEnum.NotifySubscribers).PerformTask(taskExecutionValues);
                }
            }

            if (resultDto.Errors != null)
            {
                foreach (var error in resultDto.Errors.Errors)
                {
                    LogginingService.LogError($"ERROR executing task {taskExecuteParametersDto.Id}: {error.ErrorType} - {error.ErrorMessageKey}");
                }
            }

            return resultDto;
        }

        public ResultDto<bool> TaskExecuteForm(TaskExecuteFormDto taskExecuteParametersDto, TaskExecutionFormValues taskExecutionFormValues)
        {
            LogginingService.LogInfo($"Executing task Form: {taskExecuteParametersDto.Service.TaskId}");

            var resultDto = new ResultDto<bool> { Data = true };

            var wo = _workOrdersRepository.GetByIdIncludingExecuteValues(taskExecuteParametersDto.WorkOrderId);
            var people = _peopleRepository.GetByConfigIdIncludePermissions(taskExecuteParametersDto.UserId);
            var service = _serviceRepository.GetGeneratedServiceWOForms(taskExecuteParametersDto.ServiceId).FirstOrDefault();
            var currentTask = _taskRepository.GetByIdIncludeBasicInfo(taskExecuteParametersDto.Service.TaskId.Value);

            bool taskAvailable = true;
            if (taskAvailable)
            {
                var taskExecutionValues = new TaskExecutionValues
                {
                    CurrentWorkOrder = wo,
                    CurrentPeople = people,
                    CurrentTask = currentTask,
                    TaskParameters = new TaskExecuteDto(),
                    Result = resultDto,
                    CustomerId = taskExecuteParametersDto.CustomerId,
                    CreatedService = service
                };

                resultDto = ProcessTaskExecutionForm(taskExecutionValues, taskExecutionFormValues);

                if (resultDto.Errors?.Errors == null)
                {
                    var saveResult = _workOrdersRepository.UpdateWorkOrder(taskExecutionValues.CurrentWorkOrder);
                    resultDto = ProcessResult(saveResult.IsOk, saveResult);
                    if (resultDto.Errors?.Errors == null)
                    {
                        resultDto = _taskFactory.GetTaskExecution(TaskActionTypeEnum.NotifySubscribers).PerformTask(taskExecutionValues);
                    }
                }
            }
            else
            {
                resultDto.Data = false;
                resultDto.Errors = new ErrorsDto
                {
                    Errors = new List<ErrorDto>
                    {
                        new ErrorDto
                        {
                            ErrorType = ErrorType.ValidationError,
                            ErrorMessageKey = "Task can't execute, permission denied"
                        }
                    }
                };
            }

            if (resultDto.Errors != null)
            {
                foreach (var error in resultDto.Errors.Errors)
                {
                    LogginingService.LogError($"ERROR executing task {taskExecuteParametersDto.Service.TaskId}: {error.ErrorType} - {error.ErrorMessageKey}");
                }
            }

            return resultDto;
        }

        public TaskApiDto GetCreationTasks(int peopleConfigId, WorkOrders workOrder)
        {
            var people = _peopleRepository.GetByConfigIdIncludePermissions(peopleConfigId);
            var woTypes = workOrder.WorkOrderTypes?.ToWorkOrderFatherListDto().Select(t => t.Id);
            var tasks = _taskRepository.GetWorkOrderCreationTask(people, workOrder, woTypes).FirstOrDefault();
            var tasksDto = tasks?.ToTaskApiDto();

            return tasksDto;
        }

        private ResultDto<bool> ProcessTaskExecutionFlow(TaskExecutionValues taskExecutionValues)
        {
            var resultDto = _taskFactory.GetTaskExecution(taskExecutionValues.TaskParameters.Type.ToTaskAction())?.PerformTask(taskExecutionValues) ?? taskExecutionValues.Result;
            if (resultDto.Errors?.Errors == null)
            {
                resultDto = ExecutePostconditions(taskExecutionValues);
                if (resultDto.Errors?.Errors == null)
                {
                    resultDto = _taskFactory.GetTaskExecution(TaskActionTypeEnum.CreateDerivedServices).PerformTask(taskExecutionValues);
                    if (resultDto.Errors?.Errors == null)
                    {
                        resultDto = _taskFactory.GetTaskExecution(TaskActionTypeEnum.CreateDerivedWorkOrder).PerformTask(taskExecutionValues);
                        if (resultDto.Errors?.Errors == null && taskExecutionValues.TaskParameters.Type == TaskTypeEnum.IdServeiPredefinit && taskExecutionValues.CreatedService != null)
                        {
                            resultDto = _taskFactory.GetTaskExecution(TaskActionTypeEnum.ApplyBillableRules).PerformTask(taskExecutionValues);
                        }
                        if (resultDto.Errors?.Errors == null)
                        {
                            resultDto = _taskFactory.GetTaskExecution(TaskActionTypeEnum.AddAuditory).PerformTask(taskExecutionValues);
                            if (resultDto.Errors?.Errors == null && taskExecutionValues.TaskParameters.Type == TaskTypeEnum.IdServeiPredefinit && taskExecutionValues.CreatedService != null)
                            {
                                resultDto = _taskFactory.GetTaskExecution(TaskActionTypeEnum.Analysis).PerformTask(taskExecutionValues);
                            }
                        }
                    }
                }
            }
            return resultDto;
        }

        private ResultDto<bool> ProcessTaskExecutionForm(TaskExecutionValues taskExecutionValues, TaskExecutionFormValues taskExecutionFormValues)
        {
            var resultDto = new ResultDto<bool>();
            if (taskExecutionFormValues.ExecuteBillRules)
            { 
                resultDto = _taskFactory.GetTaskExecution(TaskActionTypeEnum.ApplyBillableRules).PerformTask(taskExecutionValues);
            }

            if (taskExecutionFormValues.ExecuteAnalysis)
            { 
                resultDto = _taskFactory.GetTaskExecution(TaskActionTypeEnum.Analysis).PerformTask(taskExecutionValues);
            }

            return resultDto;
        }

        private ResultDto<bool> ExecutePostconditions(TaskExecutionValues taskExecutionValues)
        {
            var postConditionCollections = _taskRepository.GetAvailablePostConditionCollection(
                taskExecutionValues.CurrentTask.Id, taskExecutionValues.CurrentPeople,
                taskExecutionValues.CurrentWorkOrder, taskExecutionValues.WoTypes);

            foreach (var postConditions in postConditionCollections)
            {
                foreach (var postcondition in postConditions.Postconditions)
                {
                    var postconditionValues = new PostconditionExecutionValues
                    {
                        Postcondition = postcondition,
                        WorkOrder = taskExecutionValues.CurrentWorkOrder,
                        Result = taskExecutionValues.Result
                    };
                    taskExecutionValues.Result = _postconditionFactory.GetPostconditionExecution(postcondition.NameFieldModel.ToPostconditionAction())?.PerformPostcondition(postconditionValues) ?? taskExecutionValues.Result;
                    if (taskExecutionValues.Result.Errors?.Errors != null)
                    {
                        break;
                    }
                }
                if (taskExecutionValues.Result.Errors?.Errors != null)
                {
                    break;
                }
            }

            return taskExecutionValues.Result;
        }

        public TaskInfoDto GetSupplantTechnician(int peopleConfigId, TaskInfoDto taskInfoDto)
        {
            var dto = new TaskInfoDto();
            var people = _peopleRepository.GetByConfigIdIncludePermissions(peopleConfigId);
            AddTecniciansList(dto, people);
            return dto;
        }

        private void AddTecniciansList(TaskInfoDto dto, Entities.Tenant.People people)
        {
            var peopleSameSubcontract = _peopleRepository.GetBySameSubcontract(people.SubcontractId);
            var technicians = peopleSameSubcontract.ToTechnicianDto();
            dto.Technicians = technicians;
        }

        private void AddWoQueueState(TaskInfoDto taskInfo, int workOrderId, Entities.Tenant.Tasks currentTask)
        {
            var wo = _workOrdersRepository.GetByIdIncludeBasicInfo(workOrderId);
            var fromState = wo.Queue?.ToFromToDto();
            var toState = currentTask.Queue?.ToFromToDto();
            taskInfo.Changes = new FromToPlusColorDto
            {
                From = fromState,
                To = toState
            };
        }

        private void AddWoInternalChangedState(TaskInfoDto taskInfo, int workOrderId, Entities.Tenant.Tasks currentTask)
        {
            var wo = _workOrdersRepository.GetByIdIncludeBasicInfo(workOrderId);
            var fromState = wo.WorkOrderStatus?.ToFromToDto();
            var toState = currentTask.WorkOrderStatus?.ToFromToDto();
            taskInfo.Changes = new FromToPlusColorDto
            {
                From = fromState,
                To = toState,
                Color = currentTask.WorkOrderStatus?.Color
            };
        }

        private void AddWoExternalChangedState(TaskInfoDto taskInfo, int workOrderId, Entities.Tenant.Tasks currentTask)
        {
            var wo = _workOrdersRepository.GetByIdIncludeBasicInfo(workOrderId);
            var fromState = wo.ExternalWorOrderStatus?.ToFromToDto();
            var toState = currentTask.ExternalWorOrderStatus?.ToFromToDto();
            taskInfo.Changes = new FromToPlusColorDto
            {
                From = fromState,
                To = toState,
                Color = currentTask.ExternalWorOrderStatus?.Color
            };
        }

        private void AddPredefinedServiceData(TaskInfoDto dto, Entities.Tenant.Tasks currentTask, Entities.Tenant.People people)
        {
            var extraFieldTypes = _extraFieldTypesRepository.GetAll();
            dto.Service = currentTask.PredefinedService.ToPredefinedServiceTaskDto();
            var user = _userManager.Users.Include(u => u.Language).FirstOrDefault(u => u.Email == people.Email);

            foreach (var extraFieldValue in dto.Service.CollectionsExtraField.ExtraFieldValues)
            {
                var type = extraFieldTypes.FirstOrDefault(eft => eft.Id == extraFieldValue.TypeId);
                if (type != null && Enum.TryParse(type.Name, out ExtraFieldValueTypeEnum statusEnum))
                {
                    extraFieldValue.Type = statusEnum;
                }

                var translation = currentTask.PredefinedService.CollectionExtraField.CollectionsExtraFieldExtraField
                    .FirstOrDefault(cee => cee.ExtraFieldId == extraFieldValue.Id)?
                    .ExtraField.ExtraFieldsTranslations.FirstOrDefault(eft => eft.LanguageId == user.LanguageId);
                if (translation != null)
                {
                    extraFieldValue.Name = translation.NameText;
                    extraFieldValue.Description = translation.DescriptionText;
                }
            }

            var materialForms = dto.Service?.CollectionsExtraField?.ExtraFieldValues?.Where(t => t.Type == ExtraFieldValueTypeEnum.Instalation);
            if (materialForms != null && materialForms.Any())
            {
                var materials = _erpSystemInstanceQueryService.GetMaterialFormItemsFromPeople(people.Id);
                foreach (var materialForm in materialForms)
                {
                    materialForm.MaterialList = materials;
                }
            }
        }

        private Entities.Tenant.Tasks AssignPermissions(Entities.Tenant.Tasks entity, IList<int> permissionsSelected)
        {
            entity.PermissionsTasks?.Clear();
            if (permissionsSelected != null && permissionsSelected.Any())
            {
                entity.PermissionsTasks = entity.PermissionsTasks ?? new List<PermissionsTasks>();
                var permissions = _permissionsRepository.GetAllById(permissionsSelected);
                foreach (var permission in permissions)
                {
                    entity.PermissionsTasks.Add(new PermissionsTasks()
                    {
                        Permission = permission,
                    });
                }
            }
            return entity;
        }

        //TODO: Cambiar TasksTranslationDto por ContentTranslationDto
        private Entities.Tenant.Tasks UpdateTranslations(Entities.Tenant.Tasks localModel, IList<TasksTranslationsDto> translations)
        {
            localModel.TasksTranslations?.Clear();
            localModel.TasksTranslations = translations.ToTasksTranslationEntity();
            return localModel;
        }
    }
}