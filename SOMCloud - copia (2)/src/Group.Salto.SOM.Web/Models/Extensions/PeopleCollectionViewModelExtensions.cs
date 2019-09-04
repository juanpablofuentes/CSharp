using System.Collections.Generic;
using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.PeopleCollection;
using Group.Salto.SOM.Web.Models.PeopleCollection;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class PeopleCollectionViewModelExtensions
    {
        public static IList<PeopleCollectionViewModel> ToViewModel(this IList<PeopleCollectionBaseDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }

        public static PeopleCollectionViewModel ToViewModel(this PeopleCollectionBaseDto source)
        {
            PeopleCollectionViewModel result = null;
            if (source != null)
            {
                result = new PeopleCollectionViewModel();
                source.ToViewModel(result);
            }

            return result;
        }

        public static void ToViewModel(this PeopleCollectionBaseDto source, PeopleCollectionViewModel target)
        {
            if (source != null && target != null)
            {
                target.Id = source.Id;
                target.Name = source.Name;
                target.Description = source.Description;
            }
        }

        public static void ToDto(this PeopleCollectionViewModel source, PeopleCollectionBaseDto target)
        {
            if (source != null && target != null)
            {
                target.Id = source.Id;
                target.Name = source.Name;
                target.Description = source.Description;
            }
        }
    }
}