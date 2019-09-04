using Group.Salto.Common;
using System;

namespace Group.Salto.Entities.Tenant
{
    public class PeopleCostHistorical : BaseEntity
    {
        public int? PeopleId { get; set; }
        public double CostKm { get; set; }
        public DateTime Until { get; set; }

        public People People { get; set; }
    }
}
