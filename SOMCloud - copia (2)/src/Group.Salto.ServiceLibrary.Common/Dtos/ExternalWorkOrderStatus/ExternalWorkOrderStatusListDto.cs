using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos.Translation;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderStatus;

namespace Group.Salto.ServiceLibrary.Common.Dtos.ExternalWorkOrderStatus
{
    public class ExternalWorkOrderStatusListDto : WorkOrderStatusBaseDto
    {
        public IList<ContentTranslationDto> Translations { get; set; }
        public string Color { get; set; }
    }
}