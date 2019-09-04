using Group.Salto.Common;
using System;

namespace Group.Salto.Entities.Tenant
{
    public class PlanificationProcessCalendarChangeTracker : BaseEntity
    {
        public int PlanificationProcessId { get; set; }
        public DateTime? LastCheckTime { get; set; }
        public int? PersonId { get; set; }
        public int? CalendarPriority { get; set; }
        public int? CalendarId { get; set; }
        public int? EventId { get; set; }
        public DateTime? ModificationDate { get; set; }
        public bool? IsDisponible { get; set; }
    }
}
