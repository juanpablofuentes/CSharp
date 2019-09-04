using Group.Salto.ServiceLibrary.Common.Dtos.Grid;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderColumns;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Group.Salto.ServiceLibrary.Common.Contracts.WorkOrder
{
    public interface IWorkOrderQueryAdapter
    {
        string GenerateQuery(IList<WorkOrderColumnsDto> columns, GridDto gridConfig);
        string GenerateCountQuery(IList<WorkOrderColumnsDto> columns, GridDto gridConfig);
        List<SqlParameter> ParameterList { get; }
    }
}