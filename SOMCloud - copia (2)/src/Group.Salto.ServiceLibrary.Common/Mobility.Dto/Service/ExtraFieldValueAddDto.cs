using System;
using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.File;

namespace Group.Salto.ServiceLibrary.Common.Mobility.Dto.Service
{
    public class ExtraFieldValueAddDto
    {
        public int Id { get; set; }
        public int? EnterValue { get; set; }
        public DateTime? DateValue { get; set; }
        public double? DecimalValue { get; set; }
        public bool? BooleaValue { get; set; }
        public string StringValue { get; set; }
        public byte[] Signature { get; set; }
        public IEnumerable<FieldMaterialFormDto> MaterialForms { get; set; }
        public IEnumerable<RequestFileDto> Files { get; set; }
        public ExtraFieldAddDto ExtraField { get; set; }
    }
}
