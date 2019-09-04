using System.Collections.Generic;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant.ContentTranslations;

namespace Group.Salto.Entities.Tenant
{
    public class NotificationTemplate : BaseEntity
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public int? PeopleNotificationTemplateTypeId { get; set; }
        public NotificationTemplateType NotificationTemplateType { get; set; }
        public ICollection<NotificationTemplateTranslation> NotificationTemplateTranslations { get; set; }
    }
}
