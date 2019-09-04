using Group.Salto.Common.Entities;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class Companies : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public double? CostKm { get; set; }

        public ICollection<CompaniesCostHistorical> CompaniesCostHistorical { get; set; }
        public ICollection<Departments> Departments { get; set; }
        public ICollection<People> People { get; set; }
        public ICollection<WorkCenters> WorkCenters { get; set; }
    }
}
