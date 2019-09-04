using Group.Salto.Common.Entities;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class WorkCenters : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int? CountryId { get; set; }
        public int? RegionId { get; set; }
        public int? StateId { get; set; }
        public int? MunicipalityId { get; set; }
        public Companies Company { get; set; }
        public People People { get; set; }
        public ICollection<People> WorkCenterPeople { get; set; }
    }
}