using Group.Salto.ServiceLibrary.Common.Dtos.Grid;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderColumns;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.WorkOrder
{
    public interface IWorkOrderQueryForm
    {
        string CreateForm(IList<WorkOrderColumnsDto> columns, GridDto gridConfig);
        string CreateCountForm(IList<WorkOrderColumnsDto> columns, GridDto gridConfig);
    }
}