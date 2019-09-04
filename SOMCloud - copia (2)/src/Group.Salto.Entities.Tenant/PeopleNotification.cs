using System.Collections.Generic;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant.ContentTranslations;

namespace Group.Salto.Entities.Tenant
{
    public class PeopleNotification : BaseEntity
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public int PeopleId { get; set; }
        public People People { get; set; }
        public ICollection<PeopleNotificationTranslation> PeopleNotificationTranslations { get; set; }
    }
}
