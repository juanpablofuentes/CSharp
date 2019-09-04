using Group.Salto.ServiceLibrary.Common.Dtos.LiteralPreconditions;
using System.Collections.Generic;
namespace Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderCategories
{
    public class WoCategoryQueryParameters : FilterQueryParametersBase
    {
        public List<int> ProjectsIdsToMatch { get; set; }
    }
}
