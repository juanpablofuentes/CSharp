using Group.Salto.ServiceLibrary.Common.Dtos.Grid;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Excel
{
    public interface IImportToExcel
    {
        byte[] GenerateFile(IList<GridDataDto> data, IList<string> columns);
    }
}