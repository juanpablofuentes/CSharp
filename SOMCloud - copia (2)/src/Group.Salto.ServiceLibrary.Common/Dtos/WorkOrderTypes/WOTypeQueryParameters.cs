using Group.Salto.ServiceLibrary.Common.Dtos.LiteralPreconditions;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderTypes
{
    public class WOTypeQueryParameters : FilterQueryParametersBase
    {
        public List<int?> WOTypeIdsToMatch { get; set; }
        public int typeLevel { get; set; }
    }
}