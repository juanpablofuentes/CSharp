using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Origins;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Origins
{
    public interface IOriginsService
    {
        ResultDto<OriginsDto> GetById(int id);
        ResultDto<IList<OriginsDto>> GetAllFiltered(OriginsFilterDto filter);
        ResultDto<OriginsDto> CreateOrigin(OriginsDto model);
        ResultDto<OriginsDto> UpdateOrigin(OriginsDto model);
        ResultDto<bool> DeleteOrigin(int id);
        ResultDto<bool> CanDelete(int id);
        IList<BaseNameIdDto<int>> GetAllOriginKeyValues();
    }
}