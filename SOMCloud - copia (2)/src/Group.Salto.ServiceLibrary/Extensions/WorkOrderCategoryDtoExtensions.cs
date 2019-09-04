using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderCategories;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class WorkOrderCategoryDtoExtensions
    {
        public static WorkOrderCategoriesListDto ToListDto(this WorkOrderCategories source)
        {
            WorkOrderCategoriesListDto result = null;
            if (source != null)
            {
                result = new WorkOrderCategoriesListDto();
                source.ToBaseDto(result);
                result.Info = source.Info;
                result.EstimatedDuration = source.EstimatedDuration.HasValue ? source.EstimatedDuration.Value : 0;
            }
            return result;
        }

        public static void ToBaseDto(this WorkOrderCategories source, WorkOrderCategoriesListDto target)
        {
            if (source != null && target != null)
            {
                target.Id = source.Id;
                target.Name = source.Name;
                target.EstimatedDuration = source.EstimatedDuration.HasValue ? source.EstimatedDuration.Value : 0;
                target.Info = source.Info;
            }
        }

        public static IList<WorkOrderCategoriesListDto> ToListDto(this IList<WorkOrderCategories> source)
        {
            return source?.MapList(x => x.ToListDto());
        }
    }
}