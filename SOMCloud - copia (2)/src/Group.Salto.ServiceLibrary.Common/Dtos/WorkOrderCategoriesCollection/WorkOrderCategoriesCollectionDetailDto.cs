using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderCategories;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderCategoriesCollection
{
    public class WorkOrderCategoriesCollectionDetailDto : WorkOrderCategoriesCollectionDto
    {
        public IList<WorkOrderCategoriesListDto> WorkOrderCategories { get; set; }
    }
}