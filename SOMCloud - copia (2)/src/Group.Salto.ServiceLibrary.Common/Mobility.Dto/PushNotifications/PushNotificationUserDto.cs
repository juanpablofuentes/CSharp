using System;

namespace Group.Salto.ServiceLibrary.Common.Mobility.Dto.PushNotifications
{
    public class PushNotificationUserDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PushMessage { get; set; }
        public DateTime SendDate { get; set; }
    }
}
