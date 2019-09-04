using Group.Salto.Common;
using System;

namespace Group.Salto.Entities.Tenant
{
    public class CompaniesCostHistorical : BaseEntity
    {
        public int? CompanyId { get; set; }
        public double CostKm { get; set; }
        public DateTime Until { get; set; }

        public Companies Company { get; set; }
    }
}
