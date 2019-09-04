using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Contracts.Notification;

namespace Group.Salto.ServiceLibrary.Common.Mobility.Dto.PushNotifications
{
    public class PushMessageSendDto : INotificationRequest
    {
        public int PeopleId { get; set; }
        public IEnumerable<int> PeopleIds { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool OpenWoPage { get; set; }
    }
}
