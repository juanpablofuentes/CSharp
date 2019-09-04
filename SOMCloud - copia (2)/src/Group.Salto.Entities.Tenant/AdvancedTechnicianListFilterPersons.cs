namespace Group.Salto.Entities.Tenant
{
    public class AdvancedTechnicianListFilterPersons
    {
        public int TechnicianListFilterId { get; set; }
        public int PeopleId { get; set; }

        public People People { get; set; }
        public AdvancedTechnicianListFilters TechnicianListFilter { get; set; }
    }
}
