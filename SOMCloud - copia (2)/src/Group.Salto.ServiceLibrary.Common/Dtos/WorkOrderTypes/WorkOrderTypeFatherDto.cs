using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderTypes
{
    public class WorkOrderTypeFatherDto : WorkOrderTypeDto
    {
        public new IList<WorkOrderTypeFatherDto> Childs { get; set; }
        public int? FatherId { get; set; }
    }
}