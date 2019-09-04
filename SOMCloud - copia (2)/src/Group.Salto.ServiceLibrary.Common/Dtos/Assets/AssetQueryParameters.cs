using Group.Salto.ServiceLibrary.Common.Dtos.LiteralPreconditions;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Assets
{
    public class AssetQueryParameters : FilterQueryParametersBase
    {
        public List<int> LocationFinalClientIdsToMatch { get; set; }
    }
}