using Group.Salto.ServiceLibrary.Common.Contracts.PostconditionExecute;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Postconditions;
using System;
using Group.Salto.ServiceLibrary.Common.Contracts.Notification;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Enums;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.PushNotifications;

namespace Group.Salto.ServiceLibrary.Implementations.PostconditionsExecute
{
    public class ActionDatePostconditionExecution : IActionDatePostconditionExecution
    {
        private readonly IPushNotificationService _pushNotificationService;

        public ActionDatePostconditionExecution(IPushNotificationService pushNotificationService)
        {
            _pushNotificationService = pushNotificationService;
        }

        public ResultDto<bool> PerformPostcondition(PostconditionExecutionValues postconditionExecutionValues)
        {
            postconditionExecutionValues.WorkOrder.ActionDate = DateTime.UtcNow.AddMinutes(postconditionExecutionValues.Postcondition.EnterValue ?? 0);

            if (postconditionExecutionValues.WorkOrder.PeopleResponsible != null)
            {
                var processNotificationValues = new ProcessNotificationValues
                {
                    People = postconditionExecutionValues.WorkOrder.PeopleResponsible,
                    WorkOrder = postconditionExecutionValues.WorkOrder,
                    NotificationType = NotificationTemplateTypeEnum.WoNewActuationDate
                };
                _pushNotificationService.ProcessNotificationFromTemplate(processNotificationValues);
            }

            return postconditionExecutionValues.Result;
        }
    }
}
