using Group.Salto.ServiceLibrary.Common.Contracts.CollectionTypeWorkOrders;
using Group.Salto.ServiceLibrary.Common.Contracts.LiteralPreconditions;
using Group.Salto.ServiceLibrary.Common.Contracts.WOType;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderTypes;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Implementations.WOType
{
    public class WOTypeLiteralQueryResult : IWOTypeLiteralQueryResult
    {
        private ICollectionTypeWorkOrdersService _woTypeService;
        public WOTypeLiteralQueryResult(ICollectionTypeWorkOrdersService woTypeService)
        {
            _woTypeService = woTypeService ?? throw new ArgumentNullException($"{nameof(ICollectionTypeWorkOrdersService)} is null ");
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues(IFilterQueryParameters filterQueryParameters)
        {
            return _woTypeService.GetAllWOTypesKeyValues(((WOTypeQueryParameters)filterQueryParameters).WOTypeIdsToMatch);
        }
    }
}