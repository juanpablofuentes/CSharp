using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class FormConfigs : BaseEntity
    {
        public string Name { get; set; }
        public string Page { get; set; }
        public int? PageId { get; set; }

        public ICollection<FormElements> FormElements { get; set; }
        public ICollection<PlanificationProcesses> PlanificationProcessesHumanResourcesFilterNavigation { get; set; }
        public ICollection<PlanificationProcesses> PlanificationProcessesWorkOrdersFilterNavigation { get; set; }
    }
}
