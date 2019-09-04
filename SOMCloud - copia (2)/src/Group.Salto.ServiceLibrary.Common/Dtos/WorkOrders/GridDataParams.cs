using Group.Salto.Infrastructure.Common.Dto;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderColumns;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders
{
    public class GridDataParams
    {
        public List<List<DataBaseResultDto>> Data { get; set; }
        public IList<WorkOrderColumnsDto> Columns { get; set; }
        public bool IsExcelMode { get; set; }
    }
}