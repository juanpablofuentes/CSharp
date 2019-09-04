using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderCategoriesCollection;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class WorkOrderCategoriesCollectionDtoExtensions
    {
        public static WorkOrderCategoriesCollectionDto ToBaseDto(this WorkOrderCategoriesCollections source)
        {
            WorkOrderCategoriesCollectionDto result = null;
            if (source != null)
            {
                result = new WorkOrderCategoriesCollectionDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Info = source.Info,
                }; 
            }
            return result;
        }

        public static WorkOrderCategoriesCollections ToEntity(this WorkOrderCategoriesCollectionDto source)
        {
            WorkOrderCategoriesCollections result = null;
            if (source != null)
            {
                result = new WorkOrderCategoriesCollections()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Info = source.Info,
                };
            }
            return result;
        }

        public static IList<WorkOrderCategoriesCollectionDto> ToListDto(this IList<WorkOrderCategoriesCollections> source)
        {
            return source?.MapList(x => x.ToBaseDto());
        }
    }
}