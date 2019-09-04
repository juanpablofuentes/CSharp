using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class AdvancedTechnicianListFilters : BaseEntity
    {
        public TechnicianListFilters IdNavigation { get; set; }
        public ICollection<AdvancedTechnicianListFilterPersons> AdvancedTechnicianListFilterPersons { get; set; }
    }
}
