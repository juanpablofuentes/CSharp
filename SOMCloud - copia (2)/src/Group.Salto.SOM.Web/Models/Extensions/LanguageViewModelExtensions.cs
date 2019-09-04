using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.Language;
using Group.Salto.SOM.Web.Models.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class LanguageViewModelExtensions
    {
        public static LanguageViewModel ToViewModel(this LanguageDto source)
        {
            LanguageViewModel result = null;
            if (source != null)
            {
                result = new LanguageViewModel() {
                    id = source.Id,
                    Name = source.Name
                };
            }
            return result;
        }

        public static IList<LanguageViewModel> ToViewModel(this IList<LanguageDto> source)
        {
            return source?.MapList(d => d.ToViewModel());
        }
    }
}
