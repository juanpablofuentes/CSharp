using Group.Salto.Common;
using Group.Salto.Common.Entities;
using System;

namespace Group.Salto.Entities.Tenant
{
    public class PeopleCost : SoftDeleteBaseEntity
    {
        public int PeopleId { get; set; }
        public decimal? HourCost { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public People People { get; set; }
    }
}
