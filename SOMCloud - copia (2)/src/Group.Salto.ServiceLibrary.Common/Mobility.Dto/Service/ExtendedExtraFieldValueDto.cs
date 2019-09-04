using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Mobility.Dto.Service
{
    public class ExtendedExtraFieldValueDto : ExtraFieldValueDto
    {
        public int ExtraFieldId { get; set; }
        public int? Position { get; set; }
        public IEnumerable<FieldMaterialFormGetDto> MaterialList { get; set; }
        public int? TypeId { get; set; }
    }
}
