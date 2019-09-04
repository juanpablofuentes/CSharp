using System;
using Group.Salto.ServiceLibrary.Common.Dtos.Location;
using Group.Salto.ServiceLibrary.Common.Dtos.Sla;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Sla;

namespace Group.Salto.ServiceLibrary.Common.Mobility.Dto.WorkOrder
{
    public class WorkOrderBasicInfoDto
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string Site { get; set; }
        public string State { get; set; }
        public string Color { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
        public DateTime? ActionDate { get; set; }
        public string City { get; set; }
        public DateTime? ResolutionDateSla { get; set; }
        public string Equipment { get; set; }
        public int WoOpenedInSameLocation { get; set; }
        public int LocationId { get; set; }
        public LocationDto Location { get; set; }
        public bool IsWoClosed { get; set; }
        public string TextRepair { get; set; }
        public SlaBasicInfoDto Sla { get; set; }
    }
}
