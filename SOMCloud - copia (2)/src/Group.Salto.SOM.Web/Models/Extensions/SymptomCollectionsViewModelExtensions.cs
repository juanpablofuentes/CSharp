using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.SymptomCollection;
using Group.Salto.SOM.Web.Models.SymptomCollection;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class SymptomCollectionsViewModelExtensions
    {
        public static IList<SymptomCollectionViewModel> ToViewModel(this IList<SymptomCollectionBaseDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }

        public static SymptomCollectionViewModel ToViewModel(this SymptomCollectionBaseDto source)
        {
            SymptomCollectionViewModel result = null;
            if (source != null)
            {
                result = new SymptomCollectionViewModel()
                {
                     Id = source.Id,
                     Name = source.Name,
                     Element = source.Element,
                     Description = source.Description
                };                
            }

            return result;
        }
    }
}