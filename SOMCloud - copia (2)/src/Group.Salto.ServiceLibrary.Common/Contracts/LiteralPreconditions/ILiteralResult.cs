using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.LiteralPreconditions
{
    public interface ILiteralResult
    {
        IList<BaseNameIdDto<int>> GetAllKeyValues(IFilterQueryParameters filterQueryParameters);
    }
}