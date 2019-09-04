using Group.Salto.Common;
using System;

namespace Group.Salto.Entities.Tenant
{
    public class PlanificationProcesses : BaseEntity
    {
        public int ExecutionCalendar { get; set; }
        public int CheckFrequency { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StartCriteria { get; set; }
        public int? MinutesToSlaend { get; set; }
        public int WorkOrdersFilter { get; set; }
        public int HumanResourcesFilter { get; set; }
        public int MaxDuration { get; set; }
        public int Weights { get; set; }
        public DateTime? LastModification { get; set; }
        public int Horizon { get; set; }

        public Calendars ExecutionCalendarNavigation { get; set; }
        public FormConfigs HumanResourcesFilterNavigation { get; set; }
        public OptimizationFunctionWeights WeightsNavigation { get; set; }
        public FormConfigs WorkOrdersFilterNavigation { get; set; }
    }
}
