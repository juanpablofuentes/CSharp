using Group.Salto.Common;
using System;

namespace Group.Salto.Entities.Tenant
{
    public class SynchronizationSessions : BaseEntity
    {
        public string Origin { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
