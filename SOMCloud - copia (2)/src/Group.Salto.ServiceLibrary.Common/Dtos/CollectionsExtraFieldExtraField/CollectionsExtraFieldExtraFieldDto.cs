using Group.Salto.ServiceLibrary.Common.Dtos.ExtraFields;
using Group.Salto.ServiceLibrary.Common.Dtos.ExtraFieldTypes;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.CollectionsExtraFieldExtraField
{
    public class CollectionsExtraFieldExtraFieldDto
    {
        public int ExtraFieldId { get; set; }
        public int? Position { get; set; }
        public ExtraFieldsDto ExtraField { get; set; }
    }
}