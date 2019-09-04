using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class OptimizationFunctionWeights : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double TravelCost { get; set; }
        public double EconomicCostWeight { get; set; }
        public double ExternalTechnicianCostFactor { get; set; }
        public double UnattendedWopenalty { get; set; }
        public double FulfillSlaweight { get; set; }
        public double UnmetSkillPenalty { get; set; }
        public double ReplanPenalty { get; set; }
        public double ReplanPenaltyFixed { get; set; }
        public double OverMeanWorkLoadCost { get; set; }
        public double TravelSpeed { get; set; }

        public ICollection<PlanificationProcesses> PlanificationProcesses { get; set; }
    }
}
