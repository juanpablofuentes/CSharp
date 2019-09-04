using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos.Translation;

namespace Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderStatus
{
    public class WorkOrderStatusListDto : WorkOrderStatusBaseDto
    {
        public IList<ContentTranslationDto> Translations { get; set; }
        public string Color { get; set; }
        public bool IsWorkOrderClosed { get; set; }
        public bool IsPlannable { get; set; }
    }
}