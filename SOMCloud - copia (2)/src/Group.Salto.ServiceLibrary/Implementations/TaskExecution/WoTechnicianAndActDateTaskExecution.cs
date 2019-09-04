using Group.Salto.Common;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.ServiceLibrary.Common.Contracts.TaskExecution;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Tasks;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Contracts.Notification;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Enums;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.PushNotifications;

namespace Group.Salto.ServiceLibrary.Implementations.TaskExecution
{
    public class WoTechnicianAndActDateTaskExecution : IWoTechnicianAndActDateTaskExecution
    {
        private readonly IPeopleRepository _peopleRepository;
        private readonly IPushNotificationService _pushNotificationService;

        public WoTechnicianAndActDateTaskExecution(IPeopleRepository peopleRepository,
                                                   IPushNotificationService pushNotificationService)
        {
            _peopleRepository = peopleRepository;
            _pushNotificationService = pushNotificationService;
        }

        public ResultDto<bool> PerformTask(TaskExecutionValues taskExecutionValues)
        {
            var originActEndDate = taskExecutionValues.CurrentWorkOrder.ActuationEndDate;
            var originActDate = taskExecutionValues.CurrentWorkOrder.ActionDate;

            var oldPeopleId = taskExecutionValues.CurrentWorkOrder.PeopleResponsibleId;
            taskExecutionValues.CurrentWorkOrder.PeopleResponsible = _peopleRepository.GetById(taskExecutionValues.TaskParameters.TechnicianAndActuationDate.TechnicianId);
            taskExecutionValues.CurrentWorkOrder.PeopleResponsibleId = taskExecutionValues.TaskParameters.TechnicianAndActuationDate.TechnicianId;
            taskExecutionValues.CurrentWorkOrder.IsResponsibleFixed = taskExecutionValues.TaskParameters.TechnicianAndActuationDate.FixedTechnician;
            if (taskExecutionValues.TaskParameters.TechnicianAndActuationDate.ActuationDate > DateTime.MinValue)
            {
                taskExecutionValues.CurrentWorkOrder.ActionDate = taskExecutionValues.TaskParameters.TechnicianAndActuationDate.ActuationDate;
                taskExecutionValues.CurrentWorkOrder.IsActuationDateFixed = taskExecutionValues.TaskParameters.TechnicianAndActuationDate.FixedDate;

                SendPushNotifications(taskExecutionValues, oldPeopleId);
            }
            else
            {
                taskExecutionValues.Result.Data = false;
                taskExecutionValues.Result.Errors = new ErrorsDto
                {
                    Errors = new List<ErrorDto>
                    {
                        new ErrorDto
                        {
                            ErrorType = ErrorType.ValidationError,
                            ErrorMessageKey = " Task - ActionDate not set"
                        }
                    }
                };
            }

            if (originActEndDate != null && originActEndDate != DateTime.MinValue)
            {
                if (originActDate.EqualsUntilMinute(taskExecutionValues.CurrentWorkOrder.ActionDate))
                {
                    var duration = (((DateTime)originActEndDate) - (originActDate ?? DateTime.MinValue)).TotalSeconds;
                    taskExecutionValues.CurrentWorkOrder.ActuationEndDate = (taskExecutionValues.CurrentWorkOrder.ActionDate ?? DateTime.MinValue).AddSeconds(duration);
                }
            }

            return taskExecutionValues.Result;
        }

        private void SendPushNotifications(TaskExecutionValues taskExecutionValues, int? oldPeopleId)
        {
            var processNotificationValues = new ProcessNotificationValues
            {
                People = taskExecutionValues.CurrentWorkOrder.PeopleResponsible,
                WorkOrder = taskExecutionValues.CurrentWorkOrder,
                NotificationType = NotificationTemplateTypeEnum.WoNewTechnicianAndActuationDate
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
