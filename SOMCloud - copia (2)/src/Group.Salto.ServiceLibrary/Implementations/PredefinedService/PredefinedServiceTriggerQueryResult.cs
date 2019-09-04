using Group.Salto.ServiceLibrary.Common.Contracts.LiteralPreconditions;
using Group.Salto.ServiceLibrary.Common.Contracts.PredefinedServices;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Implementations.PredefinedService
{
    public class PredefinedServiceTriggerQueryResult : IPredefinedServiceTriggerQueryResult
    {
        private IPredefinedServiceService _predefinedServiceService;

        public PredefinedServiceTriggerQueryResult(IPredefinedServiceService predefinedServiceService)
        {
            _predefinedServiceService = predefinedServiceService ?? throw new ArgumentNullException($"{nameof(IPredefinedServiceService)} is null ");
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues(IFilterQueryParameters filterQueryParameters)
        {
            return _predefinedServiceService.GetAllKeyValue();
        }
    }
}