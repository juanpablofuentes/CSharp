using Group.Salto.ServiceLibrary.Common.Contracts.LiteralPreconditions;
using Group.Salto.ServiceLibrary.Common.Contracts.States;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Implementations.State
{
    public class StateLiteralQueryResult : IStateLiteralQueryResult
    {
        private IStateService _stateService;

        public StateLiteralQueryResult(IStateService stateService)
        {
            _stateService = stateService ?? throw new ArgumentNullException($"{nameof(IStateService)} is null ");
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues(IFilterQueryParameters filterQueryParameters)
        {
            return _stateService.GetAllKeyValues();
        }
    }
}