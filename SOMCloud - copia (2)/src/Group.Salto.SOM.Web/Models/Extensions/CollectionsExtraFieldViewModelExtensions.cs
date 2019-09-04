using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.CollectionsExtraField;
using Group.Salto.SOM.Web.Models.CollectionsExtraField;
using Group.Salto.SOM.Web.Models.Result;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class CollectionsExtraFieldViewModelExtensions
    {
        public static CollectionsExtraFieldViewModel ToViewModel(this CollectionsExtraFieldDto source)
        {
            CollectionsExtraFieldViewModel result = null;
            if (source != null)
            {
                result = new CollectionsExtraFieldViewModel();
                source.ToViewModel(result);
            }

            return result;
        }

        public static void ToViewModel(this CollectionsExtraFieldDto source, CollectionsExtraFieldViewModel target)
        {
            if (source != null && target != null)
            {
                target.Id = source.Id;
                target.Name = source.Name;
                target.Description = source.Description;
            }
        }

        public static IList<CollectionsExtraFieldViewModel> ToListViewModel(this IList<CollectionsExtraFieldDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }
    }
}