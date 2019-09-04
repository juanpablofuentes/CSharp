using Group.Salto.ServiceLibrary.Common.Dtos.Translation;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Tasks
{
    public class TasksDetailDto : TasksDto
    {
        public IList<ContentTranslationDto> Translations { get; set; }
    }
}