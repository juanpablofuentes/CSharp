using Group.Salto.Common;
using System;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class PushNotifications : BaseEntity
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Creator { get; set; }
        public DateTime? CreationDate { get; set; }

        public ICollection<PushNotificationsPeople> PushNotificationsPeople { get; set; }
        public ICollection<PushNotificationsPeopleCollections> PushNotificationsPeopleCollections { get; set; }
    }
}
