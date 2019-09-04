using Group.Salto.Common.Constants;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.File;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Service;
using System;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.WorkOrder
{
    public class WorkOrderExtraFieldsValuesViewModel
    {
        public int Id { get; set; }
        public string ExtraFieldValue { get; set; }
        public string ExtraFieldName { get; set; }
        public byte[] Signature { get; set; }
        public int ExtraFieldType { get; set; }
        public bool IsFile { get; set; }
        public IEnumerable<RequestFileDto> Files { get; set; }
        public IList<FieldMaterialFormDto> MaterialForm { get; set; }

        public string GetSignature()
        {
            return $"data:image;base64,{Convert.ToBase64String(Signature)}";
        }

        public bool IsSignature()
        {
            return ExtraFieldType == ((int)ExtraFieldValueTypeEnum.Signature);
        }

        public bool IsEmpty()
        {
            return ExtraFieldType != ((int)ExtraFieldValueTypeEnum.Boolea) && ExtraFieldValue == LocalizationsConstants.False;
        }

        public bool IsInstalation()
        {
            return ExtraFieldType == ((int)ExtraFieldValueTypeEnum.Instalation);
        }
    }
}