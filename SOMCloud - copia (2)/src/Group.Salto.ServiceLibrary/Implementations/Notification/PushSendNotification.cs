using System.Linq;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Agent;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.ServiceLibrary.Common.Contracts.Notification;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Enums;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.PushNotifications;

namespace Group.Salto.ServiceLibrary.Implementations.Notification
{
    public class PushSendNotification : IPushSendNotification
    {
        private readonly IPeoplePushRegistrationRepository _peoplePushRegistrationRepository;
        private readonly IDroidPushNotificationAgent _droidPushNotificationAgent;
        private readonly IIosPushNotificationAgent _iosPushNotificationAgent;

        public PushSendNotification(IPeoplePushRegistrationRepository peoplePushRegistrationRepository,
                                    IDroidPushNotificationAgent droidPushNotificationAgent,
                                    IIosPushNotificationAgent iosPushNotificationAgent)
        {
            _peoplePushRegistrationRepository = peoplePushRegistrationRepository;
            _droidPushNotificationAgent = droidPushNotificationAgent;
            _iosPushNotificationAgent = iosPushNotificationAgent;
        }
        public void SendNotification(INotificationRequest notificationRequest)
        {
            var pushData = (PushMessageSendDto)notificationRequest;
            var devices = _peoplePushRegistrationRepository.GetByPeopleIdEnabled(pushData.PeopleId);
            SendNotificationToDevices(pushData, devices);
        }

        public void SendNotificationToMultipleRecipients(INotificationRequest notificationRequest)
        {
            var pushData = (PushMessageSendDto)notificationRequest;
            var devices = _peoplePushRegistrationRepository.GetByPeopleIdsEnabled(pushData.PeopleIds);
            SendNotificationToDevices(pushData, devices);
        }

        private void SendNotificationToDevices(PushMessageSendDto pushData, IQueryable<PeoplePushRegistration> devices)
        {
            var droidClients = devices.Where(d => d.Platform == (int)DevicePlatformEnum.Android);
            var iosClients = devices.Where(d => d.Platform == (int)DevicePlatformEnum.Ios);

            _droidPushNotificationAgent.SendNotifications(droidClients.Select(d => d.PushToken), pushData.Title, pushData.Body, pushData.OpenWoPage);
            _iosPushNotificationAgent.SendNotifications(iosClients.Select(d => d.PushToken), pushData.Title, pushData.Body, pushData.OpenWoPage);
        }
    }
}
