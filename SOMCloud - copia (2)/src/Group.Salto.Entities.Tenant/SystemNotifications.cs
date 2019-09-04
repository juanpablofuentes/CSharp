using Group.Salto.Common;
using System;

namespace Group.Salto.Entities.Tenant
{
    public class SystemNotifications : BaseEntity
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime? PublicationDateTime { get; set; }
        public DateTime? VisibilityEndTime { get; set; }
        public string Type { get; set; }
        public string Creator { get; set; }
        public bool? Global { get; set; }
    }
}
