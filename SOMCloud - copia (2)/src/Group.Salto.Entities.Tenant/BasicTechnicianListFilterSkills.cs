namespace Group.Salto.Entities.Tenant
{
    public class BasicTechnicianListFilterSkills
    {
        public int TechnicianListFilterId { get; set; }
        public int SkillId { get; set; }
        public int? Level { get; set; }

        public Knowledge Skill { get; set; }
        public BasicTechnicianListFilters TechnicianListFilter { get; set; }
    }
}
