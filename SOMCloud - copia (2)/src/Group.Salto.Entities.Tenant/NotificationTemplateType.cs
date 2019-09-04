using System.Collections.Generic;
using Group.Salto.Common;

namespace Group.Salto.Entities.Tenant
{
    public class NotificationTemplateType : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<NotificationTemplate> NotificationTemplates { get; set; }
    }
}
