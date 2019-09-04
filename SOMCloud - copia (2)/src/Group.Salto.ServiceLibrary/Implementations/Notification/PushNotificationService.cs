using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common.Enums;
using Group.Salto.Entities;
using Group.Salto.Entities.Tenant;
using Group.Salto.Entities.Tenant.ContentTranslations;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Notification;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Enums;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.PushNotifications;
using Group.Salto.ServiceLibrary.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Group.Salto.ServiceLibrary.Implementations.Notification
{
    public class PushNotificationService : BaseService, IPushNotificationService
    {
        private readonly IPeopleRepository _peopleRepository;
        private readonly IPeoplePushRegistrationRepository _peoplePushRegistrationRepository;
        private readonly INotificationFactory _notificationFactory;
        private readonly UserManager<Users> _userManager;
        private readonly INotificationTemplateRepository _notificationTemplateRepository;
        private readonly INotificationTemplateTypeRepository _notificationTemplateTypeRepository;
        private readonly IPeopleNotificationRepository _peopleNotificationRepository;

        public PushNotificationService(ILoggingService logginingService, 
                                       IPeopleRepository peopleRepository,
                                       IPeoplePushRegistrationRepository peoplePushRegistrationRepository,
                                       INotificationFactory notificationFactory,
                                       UserManager<Users> userManager,
                                       INotificationTemplateRepository notificationTemplateRepository,
                                       INotificationTemplateTypeRepository notificationTemplateTypeRepository,
                                       IPeopleNotificationRepository peopleNotificationRepository) : base(logginingService)
        {
            _peopleRepository = peopleRepository;
            _peoplePushRegistrationRepository = peoplePushRegistrationRepository;
            _notificationFactory = notificationFactory;
            _userManager = userManager;
            _notificationTemplateRepository = notificationTemplateRepository;
            _notificationTemplateTypeRepository = notificationTemplateTypeRepository;
            _peopleNotificationRepository = peopleNotificationRepository;
        }

        public bool RegisterPushUserDevice(PushRegistrationDto pushRegDto, int peopleConfigId)
        {
            ResultDto<bool> result;

            var currentPeople = _peopleRepository.GetByConfigId(peopleConfigId);
            var currentRegistration = _peoplePushRegistrationRepository.GetByDeviceId(pushRegDto.DeviceId);

            if (currentRegistration != null)
            {
                currentRegistration.DeviceModel = pushRegDto.DeviceModel;
                currentRegistration.Enabled = true;
                currentRegistration.Idiom = pushRegDto.Idiom;
                currentRegistration.Manufacturer = pushRegDto.Manufacturer;
                currentRegistration.People = currentPeople;
                currentRegistration.Platform = (int)pushRegDto.Platform;
                currentRegistration.PushToken = pushRegDto.PushToken;
                currentRegistration.Version = pushRegDto.Version;
                currentRegistration.UpdateDate = DateTime.UtcNow;
                var resultRepository = _peoplePushRegistrationRepository.UpdateRegistration(currentRegistration);
                result = ProcessResult(resultRepository.IsOk, resultRepository);
            }
            else
            {
                var registration = new PeoplePushRegistration
                {
                    UpdateDate = DateTime.UtcNow,
                    DeviceId = pushRegDto.DeviceId,
                    DeviceModel = pushRegDto.DeviceModel,
                    Enabled = true,
                    Idiom = pushRegDto.Idiom,
                    Manufacturer = pushRegDto.Manufacturer,
                    People = currentPeople,
                    Platform = (int)pushRegDto.Platform,
                    PushToken = pushRegDto.PushToken,
                    Version = pushRegDto.Version
                };
                var resultRepository = _peoplePushRegistrationRepository.CreateRegistration(registration);
                result = ProcessResult(resultRepository.IsOk, resultRepository);
            }
            return result.Data;
        }

        public bool ChangePushState(PushChangeStateDto pushStateDto)
        {
            var result = false;
            var currentRegistration = _peoplePushRegistrationRepository.GetByDeviceId(pushStateDto.DeviceId);
            if (currentRegistration != null)
            {
                currentRegistration.Enabled = pushStateDto.Enabled;
                currentRegistration.UpdateDate = DateTime.UtcNow;
                var resultRepository = _peoplePushRegistrationRepository.UpdateRegistration(currentRegistration);
                result = ProcessResult(resultRepository.IsOk, resultRepository).Data;
            }
            return result;
        }

        public IEnumerable<PushNotificationUserDto> GetUserNotifications(int peopleConfigId)
        {
            var currentPeople = _peopleRepository.GetByConfigId(peopleConfigId);
            var user = _userManager.Users.Include(u => u.Language).FirstOrDefault(u => u.Email == currentPeople.Email);
            var notifications = _peopleNotificationRepository.GetByPeopleIncludeTranslations(currentPeople.Id);
            var notificationDtos = notifications.ToDto();

            foreach (var notification in notificationDtos)
            {
                var translation = notifications.FirstOrDefault(x => x.Id == notification.Id)?
                                 .PeopleNotificationTranslations.FirstOrDefault(pnt => pnt.LanguageId == user.LanguageId);
                if (translation != null)
                {
                    notification.Title = translation.NameText;
                    notification.PushMessage = translation.DescriptionText;
                }
            }

            return notificationDtos;
        }

        public void SendRandomPush(int peopleId)
        {
            var dto = new PushMessageSendDto
            {
                PeopleId = peopleId,
                Body = "Body message",
                Title = "Title message",
                OpenWoPage = false
            };
            _notificationFactory.GetService(NotificationTypeEnum.PushMobile).SendNotification(dto);
        }

        public void ProcessNotificationFromTemplate(ProcessNotificationValues processNotificationValues)
        {
            try
            {
                var notificationTemplateType = _notificationTemplateTypeRepository.GetByName(processNotificationValues.NotificationType.ToString());
                var template = _notificationTemplateRepository.GetByTypeIncludeTranslations(notificationTemplateType.Id);
                var user = _userManager.Users.Include(u => u.Language).FirstOrDefault(u => u.Email == processNotificationValues.People.Email);
                var newNotification = CreateNotificationFromTemplate(processNotificationValues, template);
                var pushMessageDto = GetPushMessageFromPeopleNotification(newNotification, user.LanguageId);
                pushMessageDto.OpenWoPage = true;
                _peopleNotificationRepository.CreatePeopleNotificationWithoutSave(newNotification);
                _notificationFactory.GetService(NotificationTypeEnum.PushMobile).SendNotification(pushMessageDto);
            }
            catch (Exception e)
            {
                LogginingService.LogException(e);
            }
        }

        public bool GetIfDeviceIsValid(int peopleConfigId, string deviceId)
        {
            bool deviceIsValid = true;

            var currentPeople = _peopleRepository.GetByConfigId(peopleConfigId);
            var peopleDevices = _peoplePushRegistrationRepository.GetByPeopleIdEnabled(currentPeople.Id);
            if (peopleDevices.Any(d => d.Enabled && d.DeviceId != deviceId))
            {
                deviceIsValid = false;
            }

            return deviceIsValid;
        }

        public bool ForceRegisterDevice(PushRegistrationDto pushRegDto, int peopleConfigId)
        {
            bool result;
            var currentPeople = _peopleRepository.GetByConfigId(peopleConfigId);
            var peopleDevices = _peoplePushRegistrationRepository.GetByPeopleId(currentPeople.Id);
            foreach (var device in peopleDevices.Where(d => d.DeviceId != pushRegDto.DeviceId))
            {
                device.Enabled = false;
            }

            var currentRegistration = _peoplePushRegistrationRepository.GetByDeviceId(pushRegDto.DeviceId);
            if (currentRegistration != null)
            {
                currentRegistration.DeviceModel = pushRegDto.DeviceModel;
                currentRegistration.Enabled = true;
                currentRegistration.Idiom = pushRegDto.Idiom;
                currentRegistration.Manufacturer = pushRegDto.Manufacturer;
                currentRegistration.People = currentPeople;
                currentRegistration.Platform = (int)pushRegDto.Platform;
                currentRegistration.Version = pushRegDto.Version;
                currentRegistration.UpdateDate = DateTime.UtcNow;
                var repositoryResult = _peoplePushRegistrationRepository.UpdateRegistration(currentRegistration);
                result = ProcessResult(repositoryResult.IsOk, repositoryResult).Data;
            }
            else
            {
                var registration = new PeoplePushRegistration
                {
                    UpdateDate = DateTime.UtcNow,
                    DeviceId = pushRegDto.DeviceId,
                    DeviceModel = pushRegDto.DeviceModel,
                    Enabled = true,
                    Idiom = pushRegDto.Idiom,
                    Manufacturer = pushRegDto.Manufacturer,
                    People = currentPeople,
                    Platform = (int)pushRegDto.Platform,
                    PushToken = pushRegDto.PushToken,
                    Version = pushRegDto.Version
                };
                var resultRepository = _peoplePushRegistrationRepository.CreateRegistration(registration);
                result = ProcessResult(resultRepository.IsOk, resultRepository).Data;
            }

            return result;
        }

        private PushMessageSendDto GetPushMessageFromPeopleNotification(PeopleNotification peopleNotification, int languageId)
        {
            var pushMessage = new PushMessageSendDto
            {
                PeopleId = peopleNotification.PeopleId,
                Title = peopleNotification.Title,
                Body = peopleNotification.Message,
            };
            var translation = peopleNotification.PeopleNotificationTranslations.FirstOrDefault(t => t.LanguageId == languageId);
            if (translation != null)
            {
                pushMessage.Title = translation.NameText;
                pushMessage.Body = translation.DescriptionText;
            }
            return pushMessage;
        }

        private PeopleNotification CreateNotificationFromTemplate(ProcessNotificationValues processNotificationValues, NotificationTemplate template)
        {
            var stringMessage = GetMessageWithValues(template.Message, processNotificationValues);
            var notification = new PeopleNotification
            {
                Title = template.Title,
                Message = stringMessage,
                People = processNotificationValues.People,
                UpdateDate = DateTime.UtcNow,
                PeopleId = processNotificationValues.People.Id,
                PeopleNotificationTranslations = new List<PeopleNotificationTranslation>()
            };
            foreach (var templateTranslation in template.NotificationTemplateTranslations)
            {
                var templateTranslationMessage = GetMessageWithValues(templateTranslation.DescriptionText, processNotificationValues);
                var peopleNotificationTranslation = new PeopleNotificationTranslation
                {
                    UpdateDate = DateTime.UtcNow,
                    PeopleNotification = notification,
                    NameText = templateTranslation.NameText,
                    DescriptionText = templateTranslationMessage,
                    LanguageId = templateTranslation.LanguageId,
                };
                notification.PeopleNotificationTranslations.Add(peopleNotificationTranslation);
            }

            return notification;
        }

        private string GetMessageWithValues(string templateMessage, ProcessNotificationValues processNotificationValues)
        {
            var message = string.Empty;
            switch (processNotificationValues.NotificationType)
            {
                case NotificationTemplateTypeEnum.WoRemoveTechnician:
                case NotificationTemplateTypeEnum.WoNewTechnician:
                    message = string.Format(templateMessage, processNotificationValues.WorkOrder.Id);
                    break;
                case NotificationTemplateTypeEnum.WoNewActuationDate:
                case NotificationTemplateTypeEnum.WoNewTechnicianAndActuationDate:
                    message = string.Format(templateMessage, processNotificationValues.WorkOrder.Id, processNotificationValues.WorkOrder.ActionDate.Value);
                    break;
            }
            return message;
        }
    }
}
