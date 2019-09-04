using Group.Salto.ServiceLibrary.Common.Dtos.CollectionsExtraFieldExtraField;
using Group.Salto.ServiceLibrary.Common.Dtos.ExtraFieldTypes;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.CollectionsExtraField
{
    public class CollectionsExtraFieldDetailDto : CollectionsExtraFieldDto
    {
        public IList<CollectionsExtraFieldExtraFieldDto> CollectionsExtraFieldExtraField { get; set; }
        public IList<ExtraFieldsTypesDto> ExtraFieldsTypes { get; set; }
    }
}