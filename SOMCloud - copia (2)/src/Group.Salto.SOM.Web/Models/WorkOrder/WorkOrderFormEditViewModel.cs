using Group.Salto.ServiceLibrary.Common.Mobility.Dto.File;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Service;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Group.Salto.SOM.Web.Models.Extensions;
using System;
using Microsoft.AspNetCore.Http;

namespace Group.Salto.SOM.Web.Models.WorkOrder
{
    public class WorkOrderFormEditViewModel
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
        public IEnumerable<SelectListItem> ForSelect { get; set; }
        public IList<RequestFileDto> Files { get; set; }
        public IFormFile SignatureImage { get; set; }
        public IList<IFormFile>UploadFiles { get; set; }

        public bool IsTextInput()
        {
            return ExtraFieldType == ((int)ExtraFieldValueTypeEnum.Text) || ExtraFieldType == ((int)ExtraFieldValueTypeEnum.Barcode);
        }

        public bool IsNumberInput()
        {
            return ExtraFieldType == ((int)ExtraFieldValueTypeEnum.Temps) || ExtraFieldType == ((int)ExtraFieldValueTypeEnum.Enter);
        }

        public bool IsDecimalInput()
        {
            return ExtraFieldType == (int)ExtraFieldValueTypeEnum.Decimal;
        }

        public bool IsBoolean()
        {
            return ExtraFieldType == ((int)ExtraFieldValueTypeEnum.Boolea);            
        }

        public bool IsSelect()
        {
            return ExtraFieldType == ((int)ExtraFieldValueTypeEnum.Select);
        }

        public bool IsSignature()
        {
            return ExtraFieldType == ((int)ExtraFieldValueTypeEnum.Signature);
        }

        public bool IsDate()
        {
            return ExtraFieldType == ((int)ExtraFieldValueTypeEnum.Data);
        }

        public string GetSignature()
        {
            return $"data:image;base64,{Convert.ToBase64String(Signature)}";
        }
        
        public IEnumerable<SelectListItem> GetElementsForSelect()
        {            
            IEnumerable<SelectListItem> ForSelect = SelectValues.Split(";").ToSelectListItemString();
            return ForSelect;
        }
    }
}
