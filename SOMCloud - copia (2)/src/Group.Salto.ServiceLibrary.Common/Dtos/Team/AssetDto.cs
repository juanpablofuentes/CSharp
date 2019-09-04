using Group.Salto.ServiceLibrary.Common.Dtos.Location;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.WorkOrder;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Team
{
    public class AssetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public string StockNumber { get; set; }
        public string TeamNumber { get; set; }
        public string Observations { get; set; }
        public int LocationClientId { get; set; }
        public int? GuaranteeId { get; set; }
        public int? LocationId { get; set; }
        public int? SubFamilyId { get; set; }
        public int? ModelId { get; set; }
        public int AssetStatusId { get; set; }
        public int? UserId { get; set; }
        public int? UsageId { get; set; }
        public ModelDto Model { get; set; }
        public GuaranteeDto Guarantee { get; set; }
        public LocationDto Location { get; set; }
        public IEnumerable<WorkOrderEquipmentDto> WorkOrders { get; set; }
        public string AssetStatus { get; set; }
        public string Family { get; set; }
        public string Subfamily { get; set; }
    }
}