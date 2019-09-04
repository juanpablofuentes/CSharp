using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Service;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.WorkOrder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class WorkOrderFormEditViewModelExtensions
    {
        public static WorkOrderFormEditViewModel ToFormViewModel (this WorkOrderExtraFieldsValuesDto source)
        {
            WorkOrderFormEditViewModel result = new WorkOrderFormEditViewModel();
            if(source != null)
            {
                result.Id = source.Id;
                result.ExtraFieldValue = source.ExtraFieldValue;
                result.ExtraFieldBoolValue = source.ExtraFieldBoolValue;
                result.ExtraFieldName = source.ExtraFieldName;
                result.Signature = source.Signature;
                result.ExtraFieldType = source.ExtraFieldType;
                result.IsFile = source.IsFile;
                result.Files = source.Files?.ToList();
                result.SelectValues = source.SelectValues;
                result.ServiceId = source.ServiceId;
            }
            return result;
        }

        public static IList<WorkOrderFormEditViewModel> ToFormViewModel(this IList<WorkOrderExtraFieldsValuesDto> source)
        {
            return source?.MapList(x => x.ToFormViewModel());
        }

        public static WorkOrderExtraFieldsValuesDto ToDto(this WorkOrderFormEditViewModel source)
        {
            WorkOrderExtraFieldsValuesDto result = null;
            if (source != null)
            {
                result = new WorkOrderExtraFieldsValuesDto()
                {
                    Id = source.Id,
                    ExtraFieldType = source.ExtraFieldType,
                    ExtraFieldValue = GetContainer(source),
                    ExtraFieldBoolValue = source.ExtraFieldBoolValue,
                    Signature = GetBytesFromSignature(source),
                    UploadFiles = source.UploadFiles,
                    Files = source.Files?.ToList(),
                };
            }
            return result;
        }

        public static IList<WorkOrderExtraFieldsValuesDto> ToDto(this IList<WorkOrderFormEditViewModel> source)
        {
            return source?.MapList(x => x.ToDto());
        }

        private static byte[] GetBytesFromSignature(WorkOrderFormEditViewModel source)
        {
            if (source.ExtraFieldType == (int)ExtraFieldValueTypeEnum.Signature && source.SignatureImage != null){
                MemoryStream ms = new MemoryStream();
                source.SignatureImage.CopyTo(ms);
                var fileBytes = ms.ToArray();
                return fileBytes;
            }
            return null;
        }

        private static string GetContainer(WorkOrderFormEditViewModel source)
        {
            if(source.UploadFiles != null && source.ExtraFieldValue == null)
            {
                source.ExtraFieldValue = Guid.NewGuid().ToString();
            }
            return source.ExtraFieldValue;
        }
    }
}