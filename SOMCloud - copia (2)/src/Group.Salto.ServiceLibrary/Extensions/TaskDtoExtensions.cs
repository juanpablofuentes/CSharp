using System;
using System.Collections.Generic;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Tasks;
using Group.Salto.ServiceLibrary.Common.Dtos.Trigger;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Tasks;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class TaskDtoExtensions
    {
        public static TasksDto ToDto(this Tasks source)
        {
            TasksDto result = null;
            if (source != null)
            {
                int typeId = 0;
                string typeValue = "";

                switch (source.NameFieldModel)
                {
                    case nameof(TaskTypeEnum.WOReopening):
                    case nameof(TaskTypeEnum.DataTancamentClient):
                    case nameof(TaskTypeEnum.ReopenOT):
                    case nameof(TaskTypeEnum.TechnicianAndActuationDate):
                    case nameof(TaskTypeEnum.DataAssignacio):
                    case nameof(TaskTypeEnum.Creacio):
                    case nameof(TaskTypeEnum.AccountingClosingDate):
                    case nameof(TaskTypeEnum.DataTancamentOTClient):
                    case nameof(TaskTypeEnum.RestartSLAWatch):
                    case nameof(TaskTypeEnum.StopSLAWatch):
                    case nameof(TaskTypeEnum.DataActuacio):
                        typeValue = string.Empty;
                        typeId =  0;
                        break;

                    case nameof(TaskTypeEnum.EstatOTExtern):
                        typeValue = source.ExternalWorOrderStatus?.Name ?? string.Empty;
                        typeId = source.ExternalWorOrderStatusId.HasValue ? source.ExternalWorOrderStatusId.Value : 0;
                        break;

                    case nameof(TaskTypeEnum.Cua):
                        typeValue = source.Queue?.Name ?? string.Empty;
                        typeId = source.QueueId.HasValue ? source.QueueId.Value : 0;
                        break;

                    case nameof(TaskTypeEnum.Tecnic):
                        typeValue = source.PeopleTechnician?.Name ?? string.Empty;
                        typeId = source.PeopleTechnicianId.HasValue ? source.PeopleTechnicianId.Value : 0;
                        break;

                    case nameof(TaskTypeEnum.IdServeiPredefinit):
                        typeValue = source.PredefinedService?.Name ?? string.Empty;
                        typeId = source.PredefinedServiceId.HasValue ? source.WorkOrderStatusId.Value : 0;
                        break;

                    case nameof(TaskTypeEnum.EstatOT):
                        typeValue = source.WorkOrderStatus?.Name ?? string.Empty;
                        typeId = source.WorkOrderStatusId.HasValue ? source.WorkOrderStatusId.Value : 0;
                        break;

                    case nameof(TaskTypeEnum.NoAction):
                        typeValue = string.Empty;
                        typeId = 0;
                        break;
                }

                result = new TasksDto()
                {
                    TaskId = source.Id,
                    FlowId = source.FlowId,
                    Name = source.Name,
                    Description = source.Description,
                    NameFieldModel = source.NameFieldModel,
                    PermissionsTasksSelected = source.ToPermissionsTasksSelectedList(),
                    TasksPlainTranslations = source.TasksTranslations.ToDto(),
                    TriggerTypesId = source.TriggerTypesId,
                    TypeId = typeId,
                    TypeValue = typeValue,
                };
            }

            return result;
        }
        //TODO: sacar a una extensión propia.
        public static List<int> ToPermissionsTasksSelectedList(this Tasks source)
        {
            var ProfilesList = new List<int>();
            foreach (var pt in source.PermissionsTasks)
            {
                ProfilesList.Add(pt.PermissionId);
            }
            return ProfilesList;
        }

        public static TaskApiDto ToTaskApiDto(this Tasks dbModel)
        {
            var dto = new TaskApiDto
            {
                Name = dbModel.Name,
                Id = dbModel.Id
            };
            if (Enum.TryParse(typeof(TaskTypeEnum), dbModel.NameFieldModel, true, out object type))
            {
                dto.Type = (TaskTypeEnum)type;
            }
            else
            {
                dto.Type = TaskTypeEnum.NoAction;
            }
            return dto;
        }

        public static IEnumerable<TaskApiDto> ToTaskApiDto(this IEnumerable<Tasks> dbModelList)
        {
            var dtoList = new List<TaskApiDto>();
            if (dbModelList != null)
            {
                foreach (var dbModel in dbModelList)
                {
                    if (dbModel != null)
                    {
                        dtoList.Add(dbModel.ToTaskApiDto());
                    }
                }
            }
            return dtoList;
        }

        public static Tasks TriggersToEntity(this TriggerDto source, Tasks local, TriggerTypeDto type)
        {
            if (source != null)
            {
                local.Id = source.TaskId;
                local.NameFieldModel = type.Description;
                local.TriggerTypesId = type.Id;
                switch (type.Description)
                {
                    case nameof(TaskTypeEnum.WOReopening):
                    case nameof(TaskTypeEnum.DataTancamentClient):
                    case nameof(TaskTypeEnum.ReopenOT):
                    case nameof(TaskTypeEnum.TechnicianAndActuationDate):
                    case nameof(TaskTypeEnum.DataAssignacio):
                    case nameof(TaskTypeEnum.Creacio):
                    case nameof(TaskTypeEnum.AccountingClosingDate):
                    case nameof(TaskTypeEnum.DataTancamentOTClient):
                    case nameof(TaskTypeEnum.RestartSLAWatch):
                    case nameof(TaskTypeEnum.StopSLAWatch):
                    case nameof(TaskTypeEnum.DataActuacio):
                    case nameof(TaskTypeEnum.NoAction):
                        local.ExternalWorOrderStatusId = null;
                        local.QueueId = null;
                        local.PeopleTechnicianId = null;
                        local.PredefinedServiceId = null;
                        local.WorkOrderStatusId = null;
                        break;

                    case nameof(TaskTypeEnum.EstatOTExtern):
                        local.ExternalWorOrderStatusId = source.ValueId;
                        break;

                    case nameof(TaskTypeEnum.Cua):
                        local.QueueId = source.ValueId;
                        break;

                    case nameof(TaskTypeEnum.Tecnic):
                        local.PeopleTechnicianId = source.ValueId;
                        break;

                    case nameof(TaskTypeEnum.IdServeiPredefinit):
                        local.PredefinedServiceId = source.ValueId;
                        break;

                    case nameof(TaskTypeEnum.EstatOT):
                        local.WorkOrderStatusId = source.ValueId;
                        break;
                }
            }
            return local;
        }

        public static Tasks ToEntity(this TasksDto source)
        {
            Tasks result = null;
            if (source != null)
            {
                result = new Tasks()
                {
                    Name = source.Name,
                    Description = source.Description,
                    TasksTranslations = source.TasksPlainTranslations.ToTasksTranslationEntity(),
                    TriggerTypesId = source.TriggerTypesId,
                    FlowId = source.FlowId,
                    NameFieldModel = source.NameFieldModel
                };
            }
            return result;
        }
    }
}