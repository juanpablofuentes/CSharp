using System.Linq;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.CollectionTypeWorkOrders;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class CollectionTypeWorkOrdersDetailDtoExtensions
    {
        public static CollectionTypeWorkOrdersDetailDto ToDto(this CollectionsTypesWorkOrders source)
        {
            CollectionTypeWorkOrdersDetailDto result = null;
            if (source != null)
            {
                result = new CollectionTypeWorkOrdersDetailDto();
                source.ToBaseDto(result);
                result.WorkOrderTypes = source.WorkOrderTypes?.ToList().GetClosingCodesDtoTree(null);
            }

            return result;
        }

        public static CollectionsTypesWorkOrders ToEntity(this CollectionTypeWorkOrdersDetailDto source)
        {
            CollectionsTypesWorkOrders result = null;
            if (source != null)
            {
                result = new CollectionsTypesWorkOrders()
                {
                    Name = source.Name,
                    Description = source.Description,
                };
                result.WorkOrderTypes = source.WorkOrderTypes.ToEntity(result);
            }
            return result;
        }
    }
}