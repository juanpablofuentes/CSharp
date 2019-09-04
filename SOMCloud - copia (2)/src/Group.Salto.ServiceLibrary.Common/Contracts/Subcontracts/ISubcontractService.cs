using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.SubContract;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Subcontracts
{
    public interface ISubContractService
    {
        ResultDto<IList<SubContractBaseDto>> GetAllFiltered(SubContractFilterDto filter);
        ResultDto<SubContractDto> GetById(int id);
        ResultDto<SubContractDto> Create(SubContractDto model);
        ResultDto<SubContractDto> Update(SubContractDto model);
        ResultDto<bool> Delete(int id);
        IList<BaseNameIdDto<int>> GetAllKeyValues();
    }
}