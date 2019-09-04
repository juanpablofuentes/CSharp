using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;

namespace Group.Salto.ServiceLibrary.Common.Contracts.PredefinedServices
{
    public interface IPredefinedServiceService
    {
        IList<BaseNameIdDto<int>> GetAllKeyValue();
        bool CanDelete(int id);
    }
}