using Group.Salto.ServiceLibrary.Common.Contracts.LiteralPreconditions;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Postcondition
{
    public interface IPostconditionResult
    {
        IList<BaseNameIdDto<int>> GetAllKeyValues(IFilterQueryParameters filterQueryParameters);
    }
}