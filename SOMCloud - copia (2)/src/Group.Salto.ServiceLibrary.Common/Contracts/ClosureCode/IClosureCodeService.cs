using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.ClosureCode;

namespace Group.Salto.ServiceLibrary.Common.Contracts.ClosureCode
{
    public interface IClosureCodeService
    {
        ResultDto<IList<ClosureCodeBaseDto>> GetAllFiltered(ClosureCodeFilterDto filter);
        ResultDto<ClosureCodeDto> GetById(int id);
        ResultDto<ClosureCodeDto> Create(ClosureCodeDto model);
        ResultDto<ClosureCodeDto> Update(ClosureCodeDto model);
        ResultDto<bool> Delete(int id);
        ResultDto<bool> CanDelete(int id);
        IList<BaseNameIdDto<int>> GetAllKeyValues();
    }
}