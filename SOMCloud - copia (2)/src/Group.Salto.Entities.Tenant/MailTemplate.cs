using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class MailTemplate : BaseEntity
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public string Subject { get; set; }

        public ICollection<Tasks> Tasks { get; set; }
    }
}