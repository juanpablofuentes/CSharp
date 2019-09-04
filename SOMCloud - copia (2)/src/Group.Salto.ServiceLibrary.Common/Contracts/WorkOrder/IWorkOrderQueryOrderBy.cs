using Group.Salto.ServiceLibrary.Common.Dtos.Grid;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderColumns;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.WorkOrder
{
    public interface IWorkOrderQueryOrderBy
    {
        string CreateOrderBy(IList<WorkOrderColumnsDto> columns, GridDto gridConfig);
    }
}