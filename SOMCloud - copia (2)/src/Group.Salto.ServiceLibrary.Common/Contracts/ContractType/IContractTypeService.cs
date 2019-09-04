using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.ContractType
{
    public interface IContractTypeService
    {
        IList<BaseNameIdDto<int>> GetAllKeyValues();
    }
}