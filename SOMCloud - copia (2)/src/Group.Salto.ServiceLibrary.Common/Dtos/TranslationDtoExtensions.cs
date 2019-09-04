using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Common.Dtos
{
    public static class TranslationDtoExtensions
    {
        public static TranslationDto ToDto(this Entities.Translation source)
        {
            TranslationDto result = null;
            if (source != null)
            {
                result = new TranslationDto()
                {
                    Key = source.Key,
                    Text = source.Text,
                };
            }
            return result;
        }

        public static IEnumerable<TranslationDto> ToDto(this IEnumerable<Entities.Translation> source)
        {
            return source?.Select(x => x.ToDto());
        }
    }
}
