using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Sla;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Sla
{
    public interface ISlaService
    {
        ResultDto<IList<SlaDto>> GetAllFiltered(SlaFilterDto filter);
        IList<BaseNameIdDto<int>> GetAllKeyValues();
        ResultDto<SlaDetailsDto> GetById(int id);
        ResultDto<SlaDetailsDto> CreateSla(SlaDetailsDto source);
        ResultDto<SlaDetailsDto> UpdateSla(SlaDetailsDto source);
        ResultDto<bool> DeleteSla(int id);
    }
}