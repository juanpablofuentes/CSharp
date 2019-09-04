using Group.Salto.ServiceLibrary.Common.Dtos.CollectionTypeWorkOrders;
using Group.Salto.SOM.Web.Models.CollectionTypeWorkOrders;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class CollectionTypeWorkOrdersFilterViewModelExtensions
    {
        public static CollectionTypeWorkOrdersFilterDto ToDto(this CollectionTypeWorkOrdersFilterViewModel source)
        {
            CollectionTypeWorkOrdersFilterDto result = null;
            if (source != null)
            {
                result = new CollectionTypeWorkOrdersFilterDto()
                {
                    Name = source.Name,
                    Description = source.Description,
                    OrderBy = source.OrderBy,
                    Asc = source.Asc,
                };
            }
            return result;
        }
    }
}