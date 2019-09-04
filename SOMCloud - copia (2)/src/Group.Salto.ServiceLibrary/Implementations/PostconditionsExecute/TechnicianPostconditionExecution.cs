using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.ServiceLibrary.Common.Contracts.Notification;
using Group.Salto.ServiceLibrary.Common.Contracts.PostconditionExecute;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Enums;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Postconditions;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.PushNotifications;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Tasks;

namespace Group.Salto.ServiceLibrary.Implementations.PostconditionsExecute
{
    public class TechnicianPostconditionExecution : ITechnicianPostconditionExecution
    {
        private readonly IPeopleRepository _peopleRepository;
        private readonly IPushNotificationService _pushNotificationService;

        public TechnicianPostconditionExecution(IPeopleRepository peopleRepository,
                                                IPushNotificationService pushNotificationService)
        {
            _peopleRepository = peopleRepository;
            _pushNotificationService = pushNotificationService;
        }

        public ResultDto<bool> PerformPostcondition(PostconditionExecutionValues postconditionExecutionValues)
        {
            var oldPeopleId = postconditionExecutionValues.WorkOrder.PeopleResponsibleId;
            if (postconditionExecutionValues.Postcondition.PeopleTechniciansId > 0)
            {
                postconditionExecutionValues.WorkOrder.PeopleResponsibleId = postconditionExecutionValues.Postcondition.PeopleTechniciansId;
                SendPushNotifications(postconditionExecutionValues, oldPeopleId);
            }
            else
            {
                postconditionExecutionValues.WorkOrder.PeopleResponsibleId = null;
                if (oldPeopleId != null)
                {
                    var oldPeople = _peopleRepository.GetById(oldPeopleId.Value);
                    var processNotificationValues = new ProcessNotificationValues
                    {
                        People = oldPeople,
                        WorkOrder = postconditionExecutionValues.WorkOrder,
                        NotificationType = NotificationTemplateTypeEnum.WoRemoveTechnician
                    };
                    _pushNotificationService.ProcessNotificationFromTemplate(processNotificationValues);
                }
            }
            return postconditionExecutionValues.Result;
        }

        private void SendPushNotifications(PostconditionExecutionValues postconditionExecutionValues, int? oldPeopleId)
        {
            if (postconditionExecutionValues.Postcondition.PeopleTechniciansId != null)
            {
                var processNotificationValues = new ProcessNotificationValues
                {
                    People = _peopleRepository.GetById(postconditionExecutionValues.Postcondition.PeopleTechniciansId.Value),
                    WorkOrder = postconditionExecutionValues.WorkOrder,
                    NotificationType = NotificationTemplateTypeEnum.WoNewTechnicianAndActuationDate
                };
                _pushNotificationService.ProcessNotificationFromTemplate(processNotificationValues);

                if (oldPeopleId != null && oldPeopleId.Value != postconditionExecutionValues.Postcondition.PeopleTechniciansId.Value)
                {
                    var oldPeople = _peopleRepository.GetById(oldPeopleId.Value);
                    processNotificationValues = new ProcessNotificationValues
                    {
                        People = oldPeople,
                        WorkOrder = postconditionExecutionValues.WorkOrder,
                        NotificationType = NotificationTemplateTypeEnum.WoRemoveTechnician
                    };
                    _pushNotificationService.ProcessNotificationFromTemplate(processNotificationValues);
                }
            }
        }
    }
}
