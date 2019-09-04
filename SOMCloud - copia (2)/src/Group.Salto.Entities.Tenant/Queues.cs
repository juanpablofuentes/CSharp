using Group.Salto.Common;
using System.Collections.Generic;
using Group.Salto.Common.Entities;

namespace Group.Salto.Entities.Tenant
{
    public class Queues : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<ExternalServicesConfiguration> ExternalServicesConfigurationAssetQueue { get; set; }
        public ICollection<ExternalServicesConfiguration> ExternalServicesConfigurationQueue { get; set; }
        public ICollection<Postconditions> Postconditions { get; set; }
        public ICollection<PreconditionsLiteralValues> PreconditionsLiteralValues { get; set; }
        public ICollection<PermissionsQueues> PermissionQueue { get; set; }
        public ICollection<Tasks> Tasks { get; set; }
        public ICollection<WorkOrders> WorkOrders { get; set; }
        public ICollection<WorkOrdersDeritative> WorkOrdersDeritative { get; set; }
        public ICollection<QueuesTranslations> QueuesTranslations { get; set; }
        public ICollection<Projects> Projects { get; set; }
    }
}