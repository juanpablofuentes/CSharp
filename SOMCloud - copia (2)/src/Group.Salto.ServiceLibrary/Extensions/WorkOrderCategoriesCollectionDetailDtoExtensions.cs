using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderCategoriesCollection;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class WorkOrderCategoriesCollectionDetailDtoExtensions
    {
        public static WorkOrderCategoriesCollectionDetailDto ToDetailDto(this WorkOrderCategoriesCollections source)
        {
            WorkOrderCategoriesCollectionDetailDto result = null;
            if (source != null)
            {
                result = new WorkOrderCategoriesCollectionDetailDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Info = source.Info,
                    WorkOrderCategories = source.WorkOrderCategories?.ToList().ToListDto()
                };
            }
            return result;
        }

        public static IList<WorkOrderCategoriesCollectionDetailDto> ToListDetailDto(this IList<WorkOrderCategoriesCollections> source)
        {
            return source?.MapList(x => x.ToDetailDto());
        }
    }
}