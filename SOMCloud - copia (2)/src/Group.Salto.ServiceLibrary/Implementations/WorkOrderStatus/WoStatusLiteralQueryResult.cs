using Group.Salto.ServiceLibrary.Common.Contracts.LiteralPreconditions;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderStatus;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.LiteralPreconditions;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Implementations.WorkOrderStatus
{
    public class WoStatusLiteralQueryResult : IWoStatusLiteralQueryResult
    {
        private IWorkOrderStatusService _workOrderStatusService;

        public WoStatusLiteralQueryResult(IWorkOrderStatusService workOrderStatusService)
        {
            _workOrderStatusService = workOrderStatusService ?? throw new ArgumentNullException($"{nameof(IWorkOrderStatusService)} is null ");
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues(IFilterQueryParameters filterQueryParameters)
        {
            return _workOrderStatusService.GetAllKeyValues(((FilterQueryParametersBase)filterQueryParameters).LanguageId);
        }
    }
}