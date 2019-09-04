using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos.CollectionTypeWorkOrders;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class CollectionTypeWorkOrdersDtoExtensions
    {
        public static CollectionTypeWorkOrdersDto ToBaseDto(this CollectionsTypesWorkOrders source)
        {
            CollectionTypeWorkOrdersDto result = null;
            if (source != null)
            {
                result = new CollectionTypeWorkOrdersDto();
                source.ToBaseDto(result);
            }
            return result;
        }

        public static void ToBaseDto(this CollectionsTypesWorkOrders source, CollectionTypeWorkOrdersDto target)
        {
            if (source != null && target != null)
            {
                target.Id = source.Id;
                target.Name = source.Name;
                target.Description = source.Description;
            }
        }

        public static IList<CollectionTypeWorkOrdersDto> ToListDto(this IList<CollectionsTypesWorkOrders> source)
        {
            return source?.MapList(x => x.ToBaseDto());
        }
    }
}