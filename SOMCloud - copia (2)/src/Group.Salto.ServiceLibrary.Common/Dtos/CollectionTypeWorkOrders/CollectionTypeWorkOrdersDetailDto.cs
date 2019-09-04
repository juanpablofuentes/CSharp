using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderTypes;

namespace Group.Salto.ServiceLibrary.Common.Dtos.CollectionTypeWorkOrders
{
    public class CollectionTypeWorkOrdersDetailDto : CollectionTypeWorkOrdersDto
    {
        public IList<WorkOrderTypeDto> WorkOrderTypes { get; set; }
    }
}