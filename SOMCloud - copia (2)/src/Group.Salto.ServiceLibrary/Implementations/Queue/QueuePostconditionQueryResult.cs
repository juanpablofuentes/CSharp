using Group.Salto.ServiceLibrary.Common.Contracts.LiteralPreconditions;
using Group.Salto.ServiceLibrary.Common.Contracts.Queue;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.LiteralPreconditions;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Implementations.Queue
{
    public class QueuePostconditionQueryResult : IQueuePostconditionQueryResult
    {
        private IQueueService _queueService;

        public QueuePostconditionQueryResult(IQueueService queueService)
        {
            _queueService = queueService ?? throw new ArgumentNullException($"{nameof(IQueueService)} is null ");
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues(IFilterQueryParameters filterQueryParameters)
        {
            return _queueService.GetAllKeyValues(((FilterQueryParametersBase)filterQueryParameters).LanguageId);
        }
    }
}