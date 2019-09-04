using Group.Salto.ServiceLibrary.Common.Contracts.ExternalWorkOrderStatus;
using Group.Salto.ServiceLibrary.Common.Contracts.LiteralPreconditions;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.LiteralPreconditions;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Implementations.ExternalWorkOrderStatus
{
    public class ExternalWoPostconditionQueryResult : IExternalWoPostconditionQueryResult
    {
        private IExternalWorkOrderStatusService _externalWorkOrderStatusService;

        public ExternalWoPostconditionQueryResult(IExternalWorkOrderStatusService externalWorkOrderStatusService)
        {
            _externalWorkOrderStatusService = externalWorkOrderStatusService ?? throw new ArgumentNullException($"{nameof(IExternalWorkOrderStatusService)} is null ");
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues(IFilterQueryParameters filterQueryParameters)
        {
            return _externalWorkOrderStatusService.GetAllKeyValues(((FilterQueryParametersBase)filterQueryParameters).LanguageId);
        }
    }
}