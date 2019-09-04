using Group.Salto.ServiceLibrary.Common.Dtos.Grid;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Group.Salto.ServiceLibrary.Common.Contracts.WorkOrder
{
    public interface IWorkOrderQueryWhere
    {
        string CreateWhere(GridDto gridConfig, List<SqlParameter> parameterList);
    }
}