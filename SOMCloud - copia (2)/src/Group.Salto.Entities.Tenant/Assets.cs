using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class Assets : BaseEntity
    {
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public string StockNumber { get; set; }
        public string AssetNumber { get; set; }
        public string Observations { get; set; }
        public int LocationClientId { get; set; }
        public int? GuaranteeId { get; set; }
        public int? LocationId { get; set; }
        public int? SubFamilyId { get; set; }
        public int? ModelId { get; set; }
        public int AssetStatusId { get; set; }
        public int? UserId { get; set; }
        public int? UsageId { get; set; }

        public AssetStatuses AssetStatus { get; set; }
        public Guarantee Guarantee { get; set; }
        public Locations Location { get; set; }
        public Locations LocationClient { get; set; }
        public Models Model { get; set; }
        public SubFamilies SubFamily { get; set; }
        public Usages Usage { get; set; }
        public SiteUser User { get; set; }
        public ICollection<AssetsAudit> AssetsAudit { get; set; }
        public ICollection<MaterialForm> MaterialForm { get; set; }
        public ICollection<PreconditionsLiteralValues> PreconditionsLiteralValues { get; set; }
        public ICollection<AssetsHiredServices> AssetsHiredServices { get; set; }
        public ICollection<AssetsWorkOrders> AssetsWorkOrders { get; set; }
        public ICollection<WorkOrders> WorkOrders { get; set; }
        public ICollection<WorkOrdersDeritative> WorkOrdersDeritative { get; set; }        
        public ICollection<AssetsContracts> AssetsContracts { get; set; }
        public ICollection<WarehouseMovementEndpoints> WarehouseMovementEndpoints { get; set; }
    }
}
