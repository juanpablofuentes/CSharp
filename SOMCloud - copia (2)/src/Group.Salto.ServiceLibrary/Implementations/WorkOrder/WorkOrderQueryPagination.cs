using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrder;
using Group.Salto.ServiceLibrary.Common.Dtos.Grid;
using System;

namespace Group.Salto.ServiceLibrary.Implementations.WorkOrder
{
    public class WorkOrderQueryPagination : IWorkOrderQueryPagination
    {
        public string CreatePagination(GridDto gridConfig)
        {
            if (!gridConfig.ExportAllToExcel)
            {
                return $"{Environment.NewLine} OFFSET {gridConfig.Pagination.Skip} ROWS FETCH NEXT {gridConfig.Pagination.Take} ROWS ONLY";
            }
            else
            {
                return $"{Environment.NewLine} OFFSET {gridConfig.Pagination.Skip} ROWS FETCH NEXT 10000 ROWS ONLY";
            }
        }
    }
}