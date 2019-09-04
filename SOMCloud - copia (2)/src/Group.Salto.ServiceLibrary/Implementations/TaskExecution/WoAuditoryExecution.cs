using System;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.ServiceLibrary.Common.Contracts.TaskExecution;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Tasks;

namespace Group.Salto.ServiceLibrary.Implementations.TaskExecution
{
    public class WoAuditoryExecution : IWoAuditoryExecution
    {
        private readonly IPeopleRepository _peopleRepository;

        public WoAuditoryExecution(IPeopleRepository peopleRepository)
        {
            _peopleRepository = peopleRepository;
        }

        public ResultDto<bool> PerformTask(TaskExecutionValues taskExecutionValues)
        {
            if (!string.IsNullOrWhiteSpace(taskExecutionValues.TaskParameters.Observations))
            {
                taskExecutionValues.CurrentWorkOrder.Observations += $"{Environment.NewLine}{taskExecutionValues.TaskParameters.Observations}";
            }

            var audit = new Audits
            {
                DataHora = DateTime.UtcNow,
                Observations = taskExecutionValues.TaskParameters.Observations,
                Task = taskExecutionValues.CurrentTask,
                UpdateDate = DateTime.UtcNow,
                WorkOrder = taskExecutionValues.CurrentWorkOrder,
                Name = taskExecutionValues.CurrentTask.Name,
                Latitude = taskExecutionValues.TaskParameters.Latitude,
                Longitude = taskExecutionValues.TaskParameters.Longitude,
                UserConfiguration = taskExecutionValues.CurrentPeople.UserConfiguration,
                Origin = string.Empty
            };

            if (taskExecutionValues.TaskParameters.ResponsibleId > 0)
            {
                var newPeople = _peopleRepository.GetById(taskExecutionValues.TaskParameters.ResponsibleId);
                if (newPeople?.UserConfigurationId != null)
                {
                    audit.UserConfigurationId = newPeople.UserConfigurationId.Value;
                    if (taskExecutionValues.CurrentPeople.UserConfigurationId != null)
                    {
                        audit.UserConfigurationSupplanterId = taskExecutionValues.CurrentPeople.UserConfigurationId.Value;
                    }
                }
            }

            taskExecutionValues.CurrentWorkOrder.Audits.Add(audit);

            return taskExecutionValues.Result;
        }
    }
}
