using Group.Salto.Common;

namespace Group.Salto.Entities.Tenant
{
    public class PlanificationCriterias : BaseEntity
    {
        public string Name { get; set; }
        public int PeopleId { get; set; }
        public bool AllowTollRoads { get; set; }
        public decimal TollCostFactor { get; set; }
        public decimal TechnicianCostFactor { get; set; }
        public decimal ExternalTechnicianCostFactor { get; set; }
        public decimal UnmetSkillOverhead { get; set; }
        public decimal ReplanPenalty { get; set; }
        public decimal ReplanPenaltyFixed { get; set; }
        public decimal UnattendedWopenalty { get; set; }
        public int MaxExecutionTime { get; set; }
        public decimal OverMeanWorkLoadCost { get; set; }
        public decimal EconomicCostWeight { get; set; }
        public decimal FullfillSlaweight { get; set; }

        public People People { get; set; }
    }
}
