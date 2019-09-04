using Group.Salto.ServiceLibrary.Common.Contracts.LiteralPreconditions;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderStatus;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.LiteralPreconditions;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Implementations.WorkOrderStatus
{
    public class WOStatusTriggerQueryResult : IWOStatusTriggerQueryResult
    {
        private IWorkOrderStatusService _woStatusService;

        public WOStatusTriggerQueryResult(IWorkOrderStatusService woStatusService)
        {
            _woStatusService = woStatusService ?? throw new ArgumentNullException($"{nameof(IWorkOrderStatusService)} is null ");
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues(IFilterQueryParameters filterQueryParameters)
        {
            return _woStatusService.GetAllKeyValues(((FilterQueryParametersBase)filterQueryParameters).LanguageId);
        }
    }
}