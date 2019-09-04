using Group.Salto.ServiceLibrary.Common.Contracts.FinalClients;
using Group.Salto.ServiceLibrary.Common.Contracts.LiteralPreconditions;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Implementations.FinalClients
{
    public class FinalClientLiteralQueryResult : IFinalClientLiteralQueryResult
    {
        private IFinalClientsServices _finalClientsServices;

        public FinalClientLiteralQueryResult(IFinalClientsServices finalClientsServices)
        {
            _finalClientsServices = finalClientsServices ?? throw new ArgumentNullException($"{nameof(IFinalClientsServices)} is null ");
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues(IFilterQueryParameters filterQueryParameters)
        {
            return _finalClientsServices.GetAllKeyValues();
        }
    }
}