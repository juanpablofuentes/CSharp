using Group.Salto.ServiceLibrary.Common.Dtos.Grid;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderColumns;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders
{
    public class WorkOrderResultDto
    {
        public int TotalRegs { get; set; }
        public IList<WorkOrderColumnsDto> Columns { get; set; }
        public IList<GridDataDto> Data { get; set; }
    }
}