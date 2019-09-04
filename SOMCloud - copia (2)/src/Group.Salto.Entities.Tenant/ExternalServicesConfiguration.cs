using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public partial class ExternalServicesConfiguration : BaseEntity
    {
        public string ExternalService { get; set; }
        public int? ProjectId { get; set; }
        public int? WoCategoryId { get; set; }
        public int? WoStatusId { get; set; }
        public int? WoExternalStatusId { get; set; }
        public int? QueueId { get; set; }
        public int? FlowId { get; set; }
        public int? TaskId { get; set; }
        public int? FinalClientId { get; set; }
        public int? LocationId { get; set; }
        public int? AssetWoStatusId { get; set; }
        public int? AssetWoExternalStatusId { get; set; }
        public int? AssetQueueId { get; set; }

        public Queues AssetQueue { get; set; }
        public ExternalWorOrderStatuses AssetWoExternalStatus { get; set; }
        public WorkOrderStatuses AssetWoStatus { get; set; }
        public FinalClients FinalClient { get; set; }
        public Flows Flow { get; set; }
        public Locations Location { get; set; }
        public Projects Project { get; set; }
        public Queues Queue { get; set; }
        public Tasks Task { get; set; }
        public WorkOrderCategories WoCategory { get; set; }
        public ExternalWorOrderStatuses WoExternalStatus { get; set; }
        public WorkOrderStatuses WoStatus { get; set; }
        public ICollection<ExternalServicesConfigurationProjectCategories> ExternalServicesConfigurationProjectCategories { get; set; }
        public ICollection<ExternalServicesConfigurationSites> ExternalServicesConfigurationSites { get; set; }
    }
}
