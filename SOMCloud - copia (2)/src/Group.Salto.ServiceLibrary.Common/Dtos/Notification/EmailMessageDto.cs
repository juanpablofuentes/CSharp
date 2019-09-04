using Group.Salto.ServiceLibrary.Common.Contracts.Notification;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Notification
{
    public class EmailMessageDto : INotificationRequest
    {
        public string ConnectionString { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public IEnumerable<string> Recipients { get; set; }
    }
}