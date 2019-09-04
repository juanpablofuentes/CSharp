using Group.Salto.ServiceLibrary.Common.Dtos.Grid;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderColumns;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderColumns
{
    public interface IWorkOrderColumnsAdapter
    {
        IList<WorkOrderColumnsDto> GetConfiguredColumns(int id, GridDto gridConfig);
        IList<WorkOrderColumnsDto> GetAllColumns(int languageId);
    }
}