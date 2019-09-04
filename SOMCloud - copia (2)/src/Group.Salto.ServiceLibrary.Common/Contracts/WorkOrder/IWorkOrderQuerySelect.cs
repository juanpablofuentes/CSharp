using Group.Salto.ServiceLibrary.Common.Dtos.Grid;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderColumns;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.WorkOrder
{
    public interface IWorkOrderQuerySelect
    {
        string CreateCountSelect(IList<WorkOrderColumnsDto> columns, GridDto gridConfig);
        string CreateSelect(IList<WorkOrderColumnsDto> columns, GridDto gridConfig);
    }
}