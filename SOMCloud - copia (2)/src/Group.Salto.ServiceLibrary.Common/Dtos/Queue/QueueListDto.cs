using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos.Translation;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Queue
{
    public class QueueListDto : QueueBaseDto
    {
        public IList<ContentTranslationDto> Translations { get; set; }
        public IList<int> PermissionsSelected { get; set; }
    }
}