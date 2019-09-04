using Group.Salto.ServiceLibrary.Common.Mobility.Dto.File;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Service;
using Microsoft.AspNetCore.Http;

using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders
{
    public class WorkOrderExtraFieldsValuesDto
    {
        public int Id { get; set; }
        public int? ServiceId { get; set; }
        public string ExtraFieldValue { get; set; }
        public string ExtraFieldName { get; set; }
        public byte[] Signature { get; set; }
        public int ExtraFieldType { get; set; }
        public bool IsFile { get; set; }
        public bool ExtraFieldBoolValue { get; set; }
        public string SelectValues { get; set; }
        public IEnumerable<RequestFileDto> Files { get; set; }  
        public IList<IFormFile> UploadFiles { get; set; }
        public IList<FieldMaterialFormDto> MaterialForm { get; set; }
    }
}