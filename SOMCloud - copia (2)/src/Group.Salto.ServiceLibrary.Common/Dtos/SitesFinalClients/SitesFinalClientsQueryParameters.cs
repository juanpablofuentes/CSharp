using Group.Salto.ServiceLibrary.Common.Dtos.LiteralPreconditions;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.SitesFinalClients
{
    public class SitesFinalClientsQueryParameters : FilterQueryParametersBase
    {
        public List<int> FinalClientIdsToMatch { get; set; }
    }
}