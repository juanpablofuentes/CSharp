using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Service;
using Group.Salto.ServiceLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class WorkOrderExtraFieldsValuesDtoExtensions
    {
        public static WorkOrderExtraFieldsValuesDto ToExtraFieldListDto(this ExtraFieldsValues source)
        {
            WorkOrderExtraFieldsValuesDto result = null;
            if (source != null)
            {
                result = new WorkOrderExtraFieldsValuesDto()
                {
                    Id = source.Id,
                    ExtraFieldName = source.ExtraField.Name,
                    ServiceId = source.ServiceId,
                    ExtraFieldType = source.ExtraField.Type,
                    ExtraFieldValue = GetExtraFieldValue(source),
                    SelectValues = source.ExtraField.AllowedStringValues,
                    ExtraFieldBoolValue = source.BooleaValue.HasValue ? source.BooleaValue.Value : false,
                    IsFile = source.ExtraField.Type == (int)ExtraFieldValueTypeEnum.Fitxer,
                    Signature = source.Signature,
                    MaterialForm = source.MaterialForm?.ToList().ToMaterialFormDto(),
                };
            }
            return result;
        }

        public static IList<WorkOrderExtraFieldsValuesDto> ToExtraFieldListDto(this IList<ExtraFieldsValues> source)
        {
            return source?.MapList(x => x.ToExtraFieldListDto());
        }

        public static void UpdateExtraFieldValue(this ExtraFieldsValues target, WorkOrderExtraFieldsValuesDto source)
        {
            switch (source.ExtraFieldType)
            {
                case (int)ExtraFieldValueTypeEnum.Text:
                case (int)ExtraFieldValueTypeEnum.Select:
                case (int)ExtraFieldValueTypeEnum.Barcode:
                case (int)ExtraFieldValueTypeEnum.Instalation:
                case (int)ExtraFieldValueTypeEnum.Fitxer:
                    target.StringValue = source.ExtraFieldValue;
                    break;
                case (int)ExtraFieldValueTypeEnum.Enter:
                case (int)ExtraFieldValueTypeEnum.Temps:
                    target.EnterValue = Convert.ToInt32(source.ExtraFieldValue);
                    break;
                case (int)ExtraFieldValueTypeEnum.Decimal:
                case (int)ExtraFieldValueTypeEnum.Periode:
                    target.DecimalValue = (double?)source.ExtraFieldValue?.StringToDecimal(CultureInfo.InvariantCulture.TwoLetterISOLanguageName);
                    break;
                case (int)ExtraFieldValueTypeEnum.Boolea:
                    target.BooleaValue = source.ExtraFieldBoolValue;
                    break;
                case (int)ExtraFieldValueTypeEnum.Data:
                    target.DataValue = DateTimeZoneHelper.ToUtcByUser(Convert.ToDateTime(source.ExtraFieldValue));
                    break;
                case (int)ExtraFieldValueTypeEnum.Signature:
                    if(source.Signature != null) { 
                        target.Signature = source.Signature;
                    }
                    break;
                default:
                    break; 
            }
        }

        private static string GetExtraFieldValue(this ExtraFieldsValues source)
        {
            string result = null;
            switch (source.ExtraField.Type)
            {
                case (int)ExtraFieldValueTypeEnum.Text:
                case (int)ExtraFieldValueTypeEnum.Select:
                case (int)ExtraFieldValueTypeEnum.Barcode:
                case (int)ExtraFieldValueTypeEnum.Instalation:
                case (int)ExtraFieldValueTypeEnum.Fitxer:
                    result = source.StringValue;
                    break;
                case (int)ExtraFieldValueTypeEnum.Enter:
                case (int)ExtraFieldValueTypeEnum.Temps:
                    result = source.EnterValue.ToString();
                    break;
                case (int)ExtraFieldValueTypeEnum.Data:
                    var stringToDate = DateTime.Parse(source.DataValue?.ToString(), CultureInfo.CurrentCulture, DateTimeStyles.NoCurrentDateDefault);
                    result = DateTimeZoneHelper.ToLocalTimeByUser(stringToDate).ToString();
                    break;
                case (int)ExtraFieldValueTypeEnum.Decimal:
                case (int)ExtraFieldValueTypeEnum.Periode:
                    result = ((decimal?)source.DecimalValue)?.DecimalToString(CultureInfo.InvariantCulture.TwoLetterISOLanguageName);
                    break;               
                case (int)ExtraFieldValueTypeEnum.Boolea:
                    result = source.BooleaValue.ToString();
                    break;               
                default:
                    break;
            }
            return result;
        }
    }
}