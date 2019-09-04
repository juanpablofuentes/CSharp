using Group.Salto.Common;
using System.Collections.Generic;
using Group.Salto.Common.Entities;

namespace Group.Salto.Entities.Tenant
{
    public class ExternalWorOrderStatuses : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public string IdIcg { get; set; }

        public ICollection<ExternalServicesConfiguration> ExternalServicesConfigurationAssetWoExternalStatus { get; set; }
        public ICollection<ExternalServicesConfiguration> ExternalServicesConfigurationWoExternalStatus { get; set; }
        public ICollection<Postconditions> Postconditions { get; set; }
        public ICollection<PreconditionsLiteralValues> PreconditionsLiteralValues { get; set; }
        public ICollection<Tasks> Tasks { get; set; }
        public ICollection<WorkOrders> WorkOrders { get; set; }
        public ICollection<WorkOrdersDeritative> WorkOrdersDeritative { get; set; }
        public ICollection<ExternalWorkOrderStatusesTranslations> ExternalWorkOrderStatusesTranslations { get; set; }
    }
}