using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.PushNotifications;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Notification
{
    public interface IPushNotificationService
    {
        bool RegisterPushUserDevice(PushRegistrationDto pushRegDto, int peopleConfigId);
        bool ChangePushState(PushChangeStateDto pushStateDto);
        IEnumerable<PushNotificationUserDto> GetUserNotifications(int peopleConfigId);
        void SendRandomPush(int peopleId);
        void ProcessNotificationFromTemplate(ProcessNotificationValues processNotificationValues);
        bool GetIfDeviceIsValid(int peopleConfigId, string loginDeviceId);
        bool ForceRegisterDevice(PushRegistrationDto pushRegDto, int peopleConfigId);
    }
}
