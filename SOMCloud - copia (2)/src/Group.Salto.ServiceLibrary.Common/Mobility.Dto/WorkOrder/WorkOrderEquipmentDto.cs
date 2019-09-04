using System;
using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Service;

namespace Group.Salto.ServiceLibrary.Common.Mobility.Dto.WorkOrder
{
    public class WorkOrderEquipmentDto
    {
        public int Id { get; set; }
        public string TextRepair { get; set; }
        public string Observations { get; set; }
        public DateTime? ActionDate { get; set; }
        public IEnumerable<WoServiceDto> WoServices { get; set; }
    }
}
