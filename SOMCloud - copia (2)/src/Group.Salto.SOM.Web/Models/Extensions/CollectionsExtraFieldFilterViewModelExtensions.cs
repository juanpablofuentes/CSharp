using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.CollectionsExtraField;
using Group.Salto.SOM.Web.Models.CollectionsExtraField;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class CollectionsExtraFieldFilterViewModelExtensions
    {
        public static CollectionsExtraFieldFilterDto ToDto(this CollectionsExtraFieldFilterViewModel source)
        {
            CollectionsExtraFieldFilterDto result = null;
            if (source != null)
            {
                result = new CollectionsExtraFieldFilterDto()
                {
                    Name = source.Name,
                    OrderBy = source.OrderBy,
                    Asc = source.Asc,
                };
            }
            return result;
        }

        public static CollectionsExtraFieldFilterViewModel ToViewModel(this CollectionsExtraFieldFilterDto source)
        {
            CollectionsExtraFieldFilterViewModel result = null;
            if (source != null)
            {
                result = new CollectionsExtraFieldFilterViewModel();
                source.ToViewModel(result);
            }

            return result;
        }

        public static void ToViewModel(this CollectionsExtraFieldFilterDto source, CollectionsExtraFieldFilterViewModel target)
        {
            if (source != null && target != null)
            {
                target.Name = source.Name;
            }
        }

        public static IList<CollectionsExtraFieldFilterViewModel> ToListViewModel(this IList<CollectionsExtraFieldFilterDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }
    }
}