using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Tasks;
using Group.Salto.ServiceLibrary.Common.Dtos.Trigger;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Tasks;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Tasks
{
    public interface ITasksService
    {
        IList<BaseNameIdDto<int>> GetAllKeyValues();
        IEnumerable<TaskApiDto> GetAvailableTasksFromWoId(int peopleConfigId, int woId);
        ResultDto<bool> TaskExecute(TaskExecuteDto taskExecuteDto, int peopleIdInt, Guid customerId);
        ResultDto<bool> TaskExecute(TaskExecuteDto taskExecuteDto, int peopleIdInt, Guid customerId, WorkOrders wo);
        TaskInfoDto GetTaskEditInfo(int peopleConfigId, GetTaskDto taskId);
        TasksDto GetById(int taskId);
        TaskApiDto GetCreationTasks(int peopleConfigId, WorkOrders workOrder);
        TasksDetailDto GetByIdWithTranslations(int taskId);
        ResultDto<TasksDto> Create(TasksDto task);
        ResultDto<TasksDto> Update(TasksDto model);
        ResultDto<TasksDto> UpdateTrigger(TriggerDto model, TriggerTypeDto type);
        TaskInfoDto GetSupplantTechnician(int peopleConfigId, TaskInfoDto taskId);
        ResultDto<bool> TaskExecuteForm(TaskExecuteFormDto taskExecuteParametersDto, TaskExecutionFormValues taskExecutionFormValues);
    }
}