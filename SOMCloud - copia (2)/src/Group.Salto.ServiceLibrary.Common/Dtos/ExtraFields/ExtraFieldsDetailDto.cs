using Group.Salto.ServiceLibrary.Common.Dtos.Translation;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.ExtraFields
{
    public class ExtraFieldsDetailDto : ExtraFieldsDto
    {
        public IList<ContentTranslationDto> Translations { get; set; }
    }
}