using System.Collections.Generic;

namespace Group.Salto.Infrastructure.Common.Agent
{
    public interface IIosPushNotificationAgent
    {
        void SendNotifications(IEnumerable<string> deviceTokens, string title, string body, bool openWoPage);
    }
}
