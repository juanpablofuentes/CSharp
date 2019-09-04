using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.ErpSystemInstance
{
    public interface IErpSystemInstanceService
    {
        IList<BaseNameIdDto<int>> GetAllKeyValues();
    }
}