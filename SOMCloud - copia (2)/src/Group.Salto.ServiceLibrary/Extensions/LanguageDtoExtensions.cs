using Group.Salto.Common.Helpers;
using Group.Salto.Entities;
using Group.Salto.ServiceLibrary.Common.Dtos.Language;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class LanguageDtoExtensions
    {
        public static LanguageDto ToDto(this Language source)
        {
            LanguageDto result = null;

            if (source != null)
            {
                result = new LanguageDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                };
            }

            return result;
        }

        public static IList<LanguageDto> ToDto(this IList<Language> source)
        {
            return source.MapList(x => x.ToDto());
        }
    }
}