using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.ItemTypes
{
    public interface IItemTypesService
    {
        IList<BaseNameIdDto<int>> GetAllKeyValues();
    }
}