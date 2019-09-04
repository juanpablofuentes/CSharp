using System.Collections.Generic;
using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.CollectionTypeWorkOrders;
using Group.Salto.SOM.Web.Models.CollectionTypeWorkOrders;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class CollectionTypeWorkOrdersViewModelExtensions
    {
        public static CollectionTypeWorkOrdersViewModel ToViewModel(this CollectionTypeWorkOrdersDto source)
        {
            CollectionTypeWorkOrdersViewModel result = null;
            if (source != null)
            {
                result = new CollectionTypeWorkOrdersViewModel();
                source.ToViewModel(result);
            }

            return result;
        }

        public static void ToViewModel(this CollectionTypeWorkOrdersDto source, CollectionTypeWorkOrdersViewModel target)
        {
            if (source != null && target != null)
            {
                target.Name = source.Name;
                target.Description = source.Description;
                target.Id = source.Id;
            }
        }

        public static CollectionTypeWorkOrdersDto ToDto(this CollectionTypeWorkOrdersViewModel source)
        {
            CollectionTypeWorkOrdersDto result = null;
            if (source != null)
            {
                result = new CollectionTypeWorkOrdersDto();
                source.ToDto(result);
            }

            return result;
        }

        public static void ToDto(this CollectionTypeWorkOrdersViewModel source,
            CollectionTypeWorkOrdersDto target)
        {
            if (source != null && target != null)
            {
                target.Name = source.Name;
                target.Description = source.Description;
                target.Id = source.Id;
            }
        }

        public static IList<CollectionTypeWorkOrdersViewModel> ToListViewModel(this IList<CollectionTypeWorkOrdersDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }

        public static IList<CollectionTypeWorkOrdersDto> ToDto(this IList<CollectionTypeWorkOrdersViewModel> source)
        {
            return source?.MapList(x => x.ToDto());
        }
    }
}