using Group.Salto.Common;

namespace Group.Salto.Entities.Tenant
{
    public class TechnicianListFilters : BaseEntity
    {
        public int PeopleId { get; set; }
        public string Name { get; set; }
        public bool? Active { get; set; }

        public People People { get; set; }
        public AdvancedTechnicianListFilters AdvancedTechnicianListFilters { get; set; }
        public BasicTechnicianListFilters BasicTechnicianListFilters { get; set; }
    }
}
