using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.ServiceLibrary.Common.Contracts.Notification;
using Group.Salto.ServiceLibrary.Common.Contracts.TaskExecution;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Enums;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.PushNotifications;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Tasks;

namespace Group.Salto.ServiceLibrary.Implementations.TaskExecution
{
    public class WoTechnicianTaskExecution : IWoTechnicianTaskExecution
    {
        private readonly IPeopleRepository _peopleRepository;
        private readonly IPushNotificationService _pushNotificationService;

        public WoTechnicianTaskExecution(IPeopleRepository peopleRepository,
                                         IPushNotificationService pushNotificationService)
        {
            _peopleRepository = peopleRepository;
            _pushNotificationService = pushNotificationService;
        }

        public ResultDto<bool> PerformTask(TaskExecutionValues taskExecutionValues)
        {
            var oldPeopleId = taskExecutionValues.CurrentWorkOrder.PeopleResponsibleId;
            taskExecutionValues.CurrentWorkOrder.PeopleResponsible = _peopleRepository.GetById(taskExecutionValues.TaskParameters.TechnicianAndActuationDate.TechnicianId);
            taskExecutionValues.CurrentWorkOrder.PeopleResponsibleId = taskExecutionValues.TaskParameters.TechnicianAndActuationDate.TechnicianId;
            taskExecutionValues.CurrentWorkOrder.IsResponsibleFixed = taskExecutionValues.TaskParameters.TechnicianAndActuationDate.FixedTechnician;

            SendPushNotifications(taskExecutionValues, oldPeopleId);
            
            return taskExecutionValues.Result;
        }

        private void SendPushNotifications(TaskExecutionValues taskExecutionValues, int? oldPeopleId)
        {
            var processNotificationValues = new ProcessNotificationValues
            {
                People = taskExecutionValues.CurrentPeople,
                WorkOrder = taskExecutionValues.CurrentWorkOrder,
                NotificationType = NotificationTemplateTypeEnum.WoNewTechnician
            };
            _pushNotificationService.ProcessNotificationFromTemplate(processNotificationValues);

            if (oldPeopleId != null && oldPeopleId.Value != taskExecutionValues.CurrentWorkOrder.PeopleResponsibleId)
            {
                var oldPeople = _peopleRepository.GetById(oldPeopleId.Value);
                processNotificationValues = new ProcessNotificationValues
                {
                    People = oldPeople,
                    WorkOrder = taskExecutionValues.CurrentWorkOrder,
                    NotificationType = NotificationTemplateTypeEnum.WoRemoveTechnician
                };
                _pushNotificationService.ProcessNotificationFromTemplate(processNotificationValues);
            }
        }
    }
}
