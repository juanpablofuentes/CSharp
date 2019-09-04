using Group.Salto.ServiceLibrary.Common.Contracts.LiteralPreconditions;
using Group.Salto.ServiceLibrary.Common.Contracts.Queue;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.LiteralPreconditions;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Implementations.Queue
{
    public class QueueLiteralQueryResult : IQueueLiteralQueryResult
    {
        private IQueueService _queueService;

        public QueueLiteralQueryResult(IQueueService queueService)
        {
            _queueService = queueService ?? throw new ArgumentNullException($"{nameof(IQueueService)} is null ");
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues(IFilterQueryParameters filterQueryParameters)
        {
            FilterQueryParametersBase fqpBase = (FilterQueryParametersBase)filterQueryParameters;
            return _queueService.GetAllKeyValuesWithPermissions(fqpBase.LanguageId, fqpBase.UserId);
        }
    }
}