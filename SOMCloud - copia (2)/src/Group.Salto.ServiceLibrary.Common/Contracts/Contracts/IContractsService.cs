using Group.Salto.ServiceLibrary.Common.Dtos;
using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos.Contracts;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Contracts
{
    public interface IContractsService
    {
        ResultDto<ContractDto> GetById(int Id);
        ResultDto<IList<ContractsListDto>> GetAllFiltered(ContractsFilterDto filter);
        ResultDto<ContractDto> Create(ContractDto contract);
        ResultDto<ContractDto> Update(ContractDto contract);
        ResultDto<bool> Delete(int Id);
        IList<BaseNameIdDto<int>> GetAllKeyValues();
    }
}