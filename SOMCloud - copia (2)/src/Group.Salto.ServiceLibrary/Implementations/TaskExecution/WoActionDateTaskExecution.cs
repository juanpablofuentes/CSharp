using Group.Salto.ServiceLibrary.Common.Contracts.TaskExecution;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Tasks;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using Group.Salto.ServiceLibrary.Common.Contracts.Notification;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Enums;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.PushNotifications;

namespace Group.Salto.ServiceLibrary.Implementations.TaskExecution
{
    public class WoActionDateTaskExecution : IWoActionDateTaskExecution
    {
        private readonly IPushNotificationService _pushNotificationService;

        public WoActionDateTaskExecution(IPushNotificationService pushNotificationService)
        {
            _pushNotificationService = pushNotificationService;
        }

        public ResultDto<bool> PerformTask(TaskExecutionValues taskExecutionValues)
        {
            var originActEndDate = taskExecutionValues.CurrentWorkOrder.ActuationEndDate;
            var originActDate = taskExecutionValues.CurrentWorkOrder.ActionDate;

            taskExecutionValues.CurrentWorkOrder.ActionDate = taskExecutionValues.TaskParameters.TechnicianAndActuationDate.ActuationDate;
            taskExecutionValues.CurrentWorkOrder.IsActuationDateFixed = taskExecutionValues.TaskParameters.TechnicianAndActuationDate.FixedDate;

            if (originActEndDate != null && originActEndDate != DateTime.MinValue)
            {
                if (originActDate.EqualsUntilMinute(taskExecutionValues.CurrentWorkOrder.ActionDate))
                {
                    var duration = (((DateTime)originActEndDate) - (originActDate ?? DateTime.MinValue)).TotalSeconds;
                    taskExecutionValues.CurrentWorkOrder.ActuationEndDate = (taskExecutionValues.CurrentWorkOrder.ActionDate ?? DateTime.MinValue).AddSeconds(duration);
                }
            }

            var processNotificationValues = new ProcessNotificationValues
            {
                People = taskExecutionValues.CurrentPeople,
                WorkOrder = taskExecutionValues.CurrentWorkOrder,
                NotificationType = NotificationTemplateTypeEnum.WoNewActuationDate
            };
            _pushNotificationService.ProcessNotificationFromTemplate(processNotificationValues);

            return taskExecutionValues.Result;
        }
    }
}
