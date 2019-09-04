using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.ItemsSerialNumberStatuses
{
    public interface IItemsSerialNumberStatusesService
    {
        IList<BaseNameIdDto<int>> GetAllKeyValues();
    }
}