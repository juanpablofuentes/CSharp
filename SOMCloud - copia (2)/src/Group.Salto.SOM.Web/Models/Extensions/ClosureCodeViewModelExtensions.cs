using System.Collections.Generic;
using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.ClosureCode;
using Group.Salto.SOM.Web.Models.ClosureCode;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ClosureCodeViewModelExtensions
    {
        public static IList<ClosureCodeViewModel> ToViewModel(this IList<ClosureCodeBaseDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }

        public static ClosureCodeViewModel ToViewModel(this ClosureCodeBaseDto source)
        {
            ClosureCodeViewModel result = null;
            if (source != null)
            {
                result = new ClosureCodeViewModel();
                source.ToViewModel(result);
            }

            return result;
        }

        public static void ToViewModel(this ClosureCodeBaseDto source, ClosureCodeViewModel target)
        {
            if (source != null && target != null)
            {
                target.Id = source.Id;
                target.Name = source.Name;
                target.Description = source.Description;
            }
        }

        public static void ToDto(this ClosureCodeViewModel source, ClosureCodeBaseDto target)
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