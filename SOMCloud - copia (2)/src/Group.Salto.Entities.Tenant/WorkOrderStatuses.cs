using Group.Salto.Common;
using System.Collections.Generic;
using Group.Salto.Common.Entities;

namespace Group.Salto.Entities.Tenant
{
    public class WorkOrderStatuses : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public string IdIcg { get; set; }
        public bool? IsWoclosed { get; set; }
        public bool? IsPlannable { get; set; }

        public ICollection<ExternalServicesConfiguration> ExternalServicesConfigurationAssetWoStatus { get; set; }
        public ICollection<ExternalServicesConfiguration> ExternalServicesConfigurationWoStatus { get; set; }
        public ICollection<Postconditions> Postconditions { get; set; }
        public ICollection<PreconditionsLiteralValues> PreconditionsLiteralValues { get; set; }
        public ICollection<Projects> Projects { get; set; }
        public ICollection<Tasks> Tasks { get; set; }
        public ICollection<WorkOrders> WorkOrders { get; set; }
        public ICollection<WorkOrdersDeritative> WorkOrdersDeritative { get; set; }
        public ICollection<WorkOrderStatusesTranslations> WorkOrderStatusesTranslations { get; set; }
    }
}