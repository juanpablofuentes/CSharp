using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.BillLines;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.BillLines
{
    public interface IBillLineService
    {
        ResultDto<IList<BillLinesDto>> GetAllById(int id);
    }
}