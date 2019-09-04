using Group.Salto.ServiceLibrary.Common.Contracts.LiteralPreconditions;
using Group.Salto.ServiceLibrary.Common.Contracts.SitesFinalClients;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.SitesFinalClients;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Implementations.SitesFinalClients
{
    public class SitesFinalClientLiteralQueryResult : ISitesFinalClientLiteralQueryResult
    {
        private ISitesFinalClientsService _sitesFinalClientsService;

        public SitesFinalClientLiteralQueryResult(ISitesFinalClientsService sitesFinalClientsService)
        {
            _sitesFinalClientsService = sitesFinalClientsService ?? throw new ArgumentNullException($"{nameof(ISitesFinalClientsService)} is null ");
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues(IFilterQueryParameters filterQueryParameters)
        {
            return _sitesFinalClientsService.GetAllKeyValuesByFinalClientsIds(((SitesFinalClientsQueryParameters)filterQueryParameters).FinalClientIdsToMatch);
        }
    }
}