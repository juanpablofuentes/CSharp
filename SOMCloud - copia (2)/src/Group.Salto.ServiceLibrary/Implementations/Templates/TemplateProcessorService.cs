using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using Group.Salto.Common.Constants.Templates;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.ServiceLibrary.Common.Contracts.Templates;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Service;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.TemplateProcessor;

namespace Group.Salto.ServiceLibrary.Implementations.Templates
{
    public class TemplateProcessorService : ITemplateProcessorService
    {
        public string ProcessTemplate(TemplateProcessorValuesDto templateProcessorValues)
        {
            var htmlTemplate = WebUtility.HtmlDecode(templateProcessorValues.Template);

            htmlTemplate = CompleteInitialValuesTemplate(templateProcessorValues, htmlTemplate);
            htmlTemplate = CompleteServiceTemplate(templateProcessorValues, htmlTemplate);
            htmlTemplate = CompleteStatusTemplate(templateProcessorValues, htmlTemplate);
            htmlTemplate = CompleteTypologyTemplate(templateProcessorValues, htmlTemplate);
            htmlTemplate = CompleteLocationTemplate(templateProcessorValues, htmlTemplate);
            htmlTemplate = CompletePeopleTemplate(templateProcessorValues, htmlTemplate);
            htmlTemplate = CompleteWorkOrderTemplate(templateProcessorValues, htmlTemplate);

            return htmlTemplate;
        }

        private string CompleteInitialValuesTemplate(TemplateProcessorValuesDto templateProcessorValues, string htmlTemplate)
        {
            htmlTemplate = htmlTemplate.Replace(TemplateProcessorConstants.TemplateWoId, templateProcessorValues.WorkOrder.Id.ToString());
            htmlTemplate = htmlTemplate.Replace(TemplateProcessorConstants.TemplateWoClient, templateProcessorValues.WorkOrder.InternalIdentifier);
            htmlTemplate = htmlTemplate.Replace(TemplateProcessorConstants.TemplateWoClientSite, templateProcessorValues.WorkOrder.ExternalIdentifier);
            htmlTemplate = htmlTemplate.Replace(TemplateProcessorConstants.TemplateWoObservations, templateProcessorValues.WorkOrder.Observations);
            htmlTemplate = htmlTemplate.Replace(TemplateProcessorConstants.TemplateWoRepair, templateProcessorValues.WorkOrder.TextRepair);
            htmlTemplate = htmlTemplate.Replace(TemplateProcessorConstants.TemplateWoSiteCode, templateProcessorValues.WorkOrder.Location.Code);
            htmlTemplate = htmlTemplate.Replace(TemplateProcessorConstants.TemplateWoSiteName, templateProcessorValues.WorkOrder.Location.Name);
            if (templateProcessorValues.WorkOrder.WorkOrderCategory != null)
            {
                htmlTemplate = htmlTemplate.Replace(TemplateProcessorConstants.TemplateWoCategory, templateProcessorValues.WorkOrder.WorkOrderCategory.Name);
            }
            htmlTemplate = htmlTemplate.Replace(TemplateProcessorConstants.TemplateWoCategoryId, templateProcessorValues.WorkOrder.WorkOrderCategoryId.ToString());
            htmlTemplate = htmlTemplate.Replace(TemplateProcessorConstants.TemplateFormId, templateProcessorValues.Service.Id.ToString());
            htmlTemplate = ProcessDateTemplate(htmlTemplate, TemplateProcessorConstants.TemplateWoActionDate, templateProcessorValues.WorkOrder.ActionDate);
            htmlTemplate = ProcessDateTemplate(htmlTemplate, TemplateProcessorConstants.TemplateWoActuationEndDate, templateProcessorValues.WorkOrder.ActuationEndDate);
            return htmlTemplate;
        }

        private string CompleteWorkOrderTemplate(TemplateProcessorValuesDto templateProcessorValues, string htmlTemplate)
        {
            htmlTemplate = htmlTemplate.Replace(TemplateProcessorConstants.TemplateWoIsActuationFixed, templateProcessorValues.WorkOrder.IsActuationDateFixed.ToString());
            htmlTemplate = ProcessDateTemplate(htmlTemplate, TemplateProcessorConstants.TemplateWoDateCreation, templateProcessorValues.WorkOrder.CreationDate);
            htmlTemplate = ProcessDateTemplate(htmlTemplate, TemplateProcessorConstants.TemplateWoDateActuation, templateProcessorValues.WorkOrder.ActionDate);
            htmlTemplate = ProcessDateTemplate(htmlTemplate, TemplateProcessorConstants.TemplateWoDatePickUp, templateProcessorValues.WorkOrder.PickUpTime);
            htmlTemplate = ProcessDateTemplate(htmlTemplate, TemplateProcessorConstants.TemplateWoDateClientClosed, templateProcessorValues.WorkOrder.ClientClosingDate);
            htmlTemplate = ProcessDateTemplate(htmlTemplate, TemplateProcessorConstants.TemplateWoDateClosing, templateProcessorValues.WorkOrder.InternalClosingTime);
            htmlTemplate = ProcessDateTemplate(htmlTemplate, TemplateProcessorConstants.TemplateWoDateAssigned, templateProcessorValues.WorkOrder.AssignmentTime);
            htmlTemplate = ProcessDateTemplate(htmlTemplate, TemplateProcessorConstants.TemplateWoDateStopTimeSla, templateProcessorValues.WorkOrder.DateStopTimerSla);
            htmlTemplate = ProcessDateTemplate(htmlTemplate, TemplateProcessorConstants.TemplateWoDateSlaResponse, templateProcessorValues.WorkOrder.ResponseDateSla);
            htmlTemplate = ProcessDateTemplate(htmlTemplate, TemplateProcessorConstants.TemplateWoDateResolutionSla, templateProcessorValues.WorkOrder.ResolutionDateSla);
            htmlTemplate = ProcessDateTemplate(htmlTemplate, TemplateProcessorConstants.TemplateWoDatePenalizationWithoutResponseSla, templateProcessorValues.WorkOrder.DateUnansweredPenaltySla);
            htmlTemplate = ProcessDateTemplate(htmlTemplate, TemplateProcessorConstants.TemplateWoDatePenalizationWithoutRelosutionSla, templateProcessorValues.WorkOrder.DatePenaltyWithoutResolutionSla);
            htmlTemplate = ProcessDateTemplate(htmlTemplate, TemplateProcessorConstants.TemplateWoDateActuationEnd, templateProcessorValues.WorkOrder.ActuationEndDate);
            htmlTemplate = ProcessDateTemplate(htmlTemplate, TemplateProcessorConstants.TemplateWoDateClosingOt, templateProcessorValues.WorkOrder.ClosingOtdate);
            htmlTemplate = ProcessDateTemplate(htmlTemplate, TemplateProcessorConstants.TemplateWoDateClosingAccounting, templateProcessorValues.WorkOrder.AccountingClosingDate);
            htmlTemplate = ProcessDateTemplate(htmlTemplate, TemplateProcessorConstants.TemplateWoDateClosingClient, templateProcessorValues.WorkOrder.ClientClosingDate);
            htmlTemplate = ProcessDateTemplate(htmlTemplate, TemplateProcessorConstants.TemplateWoDateInternalCreation, templateProcessorValues.WorkOrder.InternalCreationDate);
            htmlTemplate = ProcessDateTemplate(htmlTemplate, TemplateProcessorConstants.TemplateWoDateSystemWhenOtClosed, templateProcessorValues.WorkOrder.SystemDateWhenOtclosed);
            htmlTemplate = ProcessExtraFieldTemplate(htmlTemplate, templateProcessorValues.Service, templateProcessorValues.NullStringValue, templateProcessorValues.NcalcEncapsulate);
            return htmlTemplate;
        }

        private string CompletePeopleTemplate(TemplateProcessorValuesDto templateProcessorValues, string htmlTemplate)
        {
            if (templateProcessorValues.WorkOrder.PeopleResponsible != null)
            {
                htmlTemplate = htmlTemplate.Replace(TemplateProcessorConstants.TemplateWoPersonDni, templateProcessorValues.WorkOrder.PeopleResponsible.Dni);
                htmlTemplate = htmlTemplate.Replace(TemplateProcessorConstants.TemplateWoPersonFullname, templateProcessorValues.WorkOrder.PeopleResponsible.FullName);
            }

            return htmlTemplate;
        }

        private string CompleteStatusTemplate(TemplateProcessorValuesDto templateProcessorValues, string htmlTemplate)
        {
            htmlTemplate = htmlTemplate.Replace(TemplateProcessorConstants.TemplateWoStatusId, templateProcessorValues.WorkOrder.WorkOrderStatusId.ToString());
            if (templateProcessorValues.WorkOrder.WorkOrderStatus != null)
            {
                htmlTemplate = htmlTemplate.Replace(TemplateProcessorConstants.TemplateWoStatusName, templateProcessorValues.WorkOrder.WorkOrderStatus.Name);
            }

            if (templateProcessorValues.WorkOrder.ExternalWorOrderStatus != null)
            {
                htmlTemplate = htmlTemplate.Replace(TemplateProcessorConstants.TemplateWoExternalStatusId, templateProcessorValues.WorkOrder.ExternalWorOrderStatusId.ToString());
                htmlTemplate = htmlTemplate.Replace(TemplateProcessorConstants.TemplateWoExternalStatusName, templateProcessorValues.WorkOrder.ExternalWorOrderStatus.Name);
            }

            return htmlTemplate;
        }

        private string CompleteServiceTemplate(TemplateProcessorValuesDto templateProcessorValues, string htmlTemplate)
        {
            if (templateProcessorValues.Service.ClosingCode != null)
            {
                htmlTemplate = htmlTemplate.Replace(TemplateProcessorConstants.TemplateFormClosingCode, templateProcessorValues.Service.ClosingCode.Id.ToString());
            }

            if (templateProcessorValues.Service.PeopleResponsible != null)
            {
                htmlTemplate = htmlTemplate.Replace(TemplateProcessorConstants.TemplateFormDni, templateProcessorValues.Service.PeopleResponsible.Dni);
                htmlTemplate = htmlTemplate.Replace(TemplateProcessorConstants.TemplateFormFullName, templateProcessorValues.Service.PeopleResponsible.FullName);
            }

            htmlTemplate = ProcessDateTemplate(htmlTemplate, TemplateProcessorConstants.TemplateFormCreationDate, templateProcessorValues.Service.CreationDate);
            return htmlTemplate;
        }

        private string CompleteLocationTemplate(TemplateProcessorValuesDto templateProcessorValues, string htmlTemplate)
        {
            if (templateProcessorValues.WorkOrder.Location != null)
            {
                htmlTemplate = htmlTemplate.Replace(TemplateProcessorConstants.TemplateWoLocationName, templateProcessorValues.WorkOrder.Location.Name);
                htmlTemplate = htmlTemplate.Replace(TemplateProcessorConstants.TemplateWoLocationProvince, templateProcessorValues.WorkOrder.Location.Province);
                htmlTemplate = htmlTemplate.Replace(TemplateProcessorConstants.TemplateWoLocationPostalCode, templateProcessorValues.WorkOrder.Location.PostalCode.ToString());
                htmlTemplate = htmlTemplate.Replace(TemplateProcessorConstants.TemplateWoLocationDirection,
                    templateProcessorValues.WorkOrder.Location.StreetType + " | " +
                    templateProcessorValues.WorkOrder.Location.Street + " | " +
                    templateProcessorValues.WorkOrder.Location.Number + " | " +
                    templateProcessorValues.WorkOrder.Location.Escala + " | " +
                    templateProcessorValues.WorkOrder.Location.GateNumber);
                htmlTemplate = htmlTemplate.Replace(TemplateProcessorConstants.TemplateWoLocationZone, templateProcessorValues.WorkOrder.Location.Zone);
                htmlTemplate = htmlTemplate.Replace(TemplateProcessorConstants.TemplateWoLocationSubzone, templateProcessorValues.WorkOrder.Location.Subzone);
                htmlTemplate = htmlTemplate.Replace(TemplateProcessorConstants.TemplateWoArea, templateProcessorValues.WorkOrder.Location.Area);
                htmlTemplate = htmlTemplate.Replace(TemplateProcessorConstants.TemplateWoLocationLatitude, templateProcessorValues.WorkOrder.Location.Latitude.ToString());
                htmlTemplate = htmlTemplate.Replace(TemplateProcessorConstants.TemplateWoLocationLongitude, templateProcessorValues.WorkOrder.Location.Longitude.ToString());
            }

            return htmlTemplate;
        }

        private string CompleteTypologyTemplate(TemplateProcessorValuesDto templateProcessorValues, string htmlTemplate)
        {
            htmlTemplate = htmlTemplate.Replace(TemplateProcessorConstants.TemplateWoProjectId, templateProcessorValues.WorkOrder.ProjectId.ToString());
            htmlTemplate = htmlTemplate.Replace(TemplateProcessorConstants.TemplateWoProjectName, templateProcessorValues.WorkOrder.Project.Name);
            htmlTemplate = htmlTemplate.Replace(TemplateProcessorConstants.TemplateWoTypeId, templateProcessorValues.WorkOrder.WorkOrderTypesId.ToString());
            return htmlTemplate;
        }

        private string ProcessExtraFieldTemplate(string htmlTemplate, Services service, string nullStringValue, bool ncalcEncapsulate)
        {
            var identifier = TemplateProcessorConstants.TemplateExtraFieldIdentifier;
            var identifierLength = identifier.Length;
            var indexSearchItem = 0;

            var indexItemFound = htmlTemplate.IndexOf(identifier, indexSearchItem, StringComparison.Ordinal);
            while (indexItemFound >= 0)
            {
                if (htmlTemplate[indexItemFound + identifierLength] == '[')
                {
                    var indexEndFormat = htmlTemplate.IndexOf("]", indexItemFound + identifierLength, StringComparison.Ordinal);
                    var extraFieldName = htmlTemplate.Substring(indexItemFound + identifierLength + 1, indexEndFormat - (indexItemFound + identifierLength + 1));
                    var extraFieldValue = service.ExtraFieldsValues.FirstOrDefault(efv => efv.ExtraField.Name.Trim() == extraFieldName);
                    if (extraFieldValue != null)
                    {
                        var fullId = identifier + "[" + extraFieldName + "]";
                        var fullIdLen = fullId.Length;
                        var fullIdX = htmlTemplate.IndexOf(fullId, indexSearchItem, StringComparison.Ordinal);
                        if (htmlTemplate.Length > (fullIdX + fullIdLen) && htmlTemplate[fullIdX + fullIdLen] == '[')
                        {
                            var fullIdEnd = htmlTemplate.IndexOf("]", fullIdX + fullIdLen, StringComparison.Ordinal);
                            var prop = htmlTemplate.Substring(fullIdX + fullIdLen + 1, fullIdEnd - (fullIdX + (fullIdLen + 1)));

                            if (prop == TemplateProcessorConstants.TemplateExtraFieldIsBlank)
                            {
                                htmlTemplate = htmlTemplate.Remove(fullIdX, fullIdEnd - fullIdX + 1);
                                htmlTemplate = htmlTemplate.Insert(fullIdX, extraFieldValue.StringValue == nullStringValue ? TemplateProcessorConstants.TemplateExtraFieldTrueValue : TemplateProcessorConstants.TemplateExtraFieldFalseValue);
                            }
                        }
                        switch ((ExtraFieldValueTypeEnum)extraFieldValue.ExtraField.Type)
                        {
                            case ExtraFieldValueTypeEnum.Data:
                            {
                                htmlTemplate = ProcessExtraFieldDateType(htmlTemplate, nullStringValue, identifier, extraFieldName, indexSearchItem, extraFieldValue);
                                break;
                            }
                            case ExtraFieldValueTypeEnum.Temps:
                            {
                                htmlTemplate = ProcessExtraFieldTimeType(htmlTemplate, nullStringValue, ncalcEncapsulate, identifier, extraFieldName, indexSearchItem, extraFieldValue, indexItemFound, indexEndFormat);

                                break;
                            }
                            default:
                                htmlTemplate = htmlTemplate.Remove(indexItemFound, indexEndFormat - indexItemFound + 1);
                                var stringExtraFieldValue = GetExtraFieldDefaultValue(extraFieldValue, nullStringValue, ncalcEncapsulate);
                                htmlTemplate = htmlTemplate.Insert(indexItemFound, stringExtraFieldValue);
                                break;
                        }
                    }
                }
                indexSearchItem = indexItemFound + 1;
                indexItemFound = htmlTemplate.IndexOf(identifier, indexSearchItem, StringComparison.Ordinal);
            }
            return htmlTemplate;
        }

        private string ProcessExtraFieldTimeType(string htmlTemplate, string nullStringValue, bool ncalcEncapsulate, string identifier, string extraFieldName, int indexSearchItem, ExtraFieldsValues extraFieldValue, int indexItemFound, int indexEndFormat)
        {
            var identifierSecond = identifier + "[" + extraFieldName + "]";
            var indexSecondSearchItem = htmlTemplate.IndexOf(identifierSecond, indexSearchItem, StringComparison.Ordinal);
            if (htmlTemplate.Length > indexSecondSearchItem + identifierSecond.Length)
            {
                if (htmlTemplate[indexSecondSearchItem + identifierSecond.Length] == '[')
                {
                    var indexEndFormatSecond = htmlTemplate.IndexOf("]", indexSecondSearchItem + identifierSecond.Length, StringComparison.Ordinal);
                    var format = htmlTemplate.Substring(indexSecondSearchItem + identifierSecond.Length + 1, indexEndFormatSecond - (indexSecondSearchItem + identifierSecond.Length + 1));
                    switch (format)
                    {
                        case TemplateProcessorConstants.TemplateExtraFieldHours:
                            htmlTemplate = htmlTemplate.Remove(indexSecondSearchItem, indexEndFormatSecond - indexSecondSearchItem + 1);
                            htmlTemplate = htmlTemplate.Insert(indexSecondSearchItem, extraFieldValue.EnterValue == null ? nullStringValue : ((extraFieldValue.EnterValue ?? 0) / 60).ToString());
                            break;
                        case TemplateProcessorConstants.TemplateExtraFieldHoursDecimal:
                            htmlTemplate = htmlTemplate.Remove(indexSecondSearchItem, indexEndFormatSecond - indexSecondSearchItem + 1);
                            htmlTemplate = htmlTemplate.Insert(indexSecondSearchItem, extraFieldValue.EnterValue == null ? nullStringValue : ((extraFieldValue.EnterValue ?? 0) * 1.0 / 60).ToString(CultureInfo.InvariantCulture));
                            break;
                        case TemplateProcessorConstants.TemplateExtraFieldMinute:
                            htmlTemplate = htmlTemplate.Remove(indexSecondSearchItem, indexEndFormatSecond - indexSecondSearchItem + 1);
                            htmlTemplate = htmlTemplate.Insert(indexSecondSearchItem, extraFieldValue.EnterValue == null ? nullStringValue : extraFieldValue.EnterValue.ToString());
                            break;
                        default:
                            htmlTemplate = htmlTemplate.Remove(indexSecondSearchItem, indexEndFormatSecond - indexSecondSearchItem + 1);
                            var stringExtraFieldValue = GetExtraFieldDefaultValue(extraFieldValue, nullStringValue, ncalcEncapsulate);
                            htmlTemplate = htmlTemplate.Insert(indexSecondSearchItem, stringExtraFieldValue);
                            break;
                    }
                }
                else
                {
                    htmlTemplate = htmlTemplate.Remove(indexItemFound, indexEndFormat - indexItemFound + 1);
                    var stringExtraFieldValue = GetExtraFieldDefaultValue(extraFieldValue, nullStringValue, ncalcEncapsulate);
                    htmlTemplate = htmlTemplate.Insert(indexItemFound, stringExtraFieldValue);
                }
            }
            else
            {
                htmlTemplate = htmlTemplate.Remove(indexItemFound, indexEndFormat - indexItemFound + 1);
                var stringExtraFieldValue = GetExtraFieldDefaultValue(extraFieldValue, nullStringValue, ncalcEncapsulate);
                htmlTemplate = htmlTemplate.Insert(indexItemFound, stringExtraFieldValue);
            }

            return htmlTemplate;
        }

        private string ProcessExtraFieldDateType(string htmlTemplate, string nullStringValue, string identifier, string extraFieldName, int indexSearchItem, ExtraFieldsValues extraFieldValue)
        {
            var identifierSecond = identifier + "[" + extraFieldName + "]";
            var indexSecondSearchItem = htmlTemplate.IndexOf(identifierSecond, indexSearchItem, StringComparison.Ordinal);
            if (htmlTemplate[indexSecondSearchItem + identifierSecond.Length] == '[')
            {
                var indexEndFormatSecond = htmlTemplate.IndexOf("]", indexSecondSearchItem + identifierSecond.Length, StringComparison.Ordinal);
                htmlTemplate = htmlTemplate.Remove(indexSecondSearchItem, indexEndFormatSecond - indexSecondSearchItem + 1);
                htmlTemplate = htmlTemplate.Insert(indexSecondSearchItem, extraFieldValue.DataValue != null ? extraFieldValue.DataValue.Value.ToString(TemplateProcessorConstants.TemplateStringDateFormat) : nullStringValue);
            }
            else
            {
                htmlTemplate = htmlTemplate.Remove(indexSecondSearchItem, identifierSecond.Length + 1);
                htmlTemplate = htmlTemplate.Insert(indexSecondSearchItem, extraFieldValue.DataValue != null ? extraFieldValue.DataValue.Value.ToString(TemplateProcessorConstants.TemplateStringDateFormat) : nullStringValue);
            }

            return htmlTemplate;
        }

        private string ProcessDateTemplate(string htmlTemplate, string stringKey, DateTime? dateTimeValue)
        {
            var indexSearchItem = 0;
            var indexItemFound = htmlTemplate.IndexOf(stringKey, indexSearchItem, StringComparison.Ordinal);

            while (htmlTemplate.IndexOf(stringKey, indexSearchItem, StringComparison.Ordinal) > 0)
            {
                if (htmlTemplate[indexItemFound + stringKey.Length] == '[')
                {
                    //Date with format specification
                    var indexEndDateFormat = htmlTemplate.IndexOf("]", indexItemFound + stringKey.Length, StringComparison.Ordinal);
                    var format = htmlTemplate.Substring(indexItemFound + stringKey.Length + 1, indexEndDateFormat - (indexItemFound + stringKey.Length + 1));
                    htmlTemplate = htmlTemplate.Remove(indexItemFound, indexEndDateFormat - indexItemFound + 1);
                    htmlTemplate = htmlTemplate.Insert(indexItemFound, dateTimeValue != null ? dateTimeValue.Value.ToString(format) : string.Empty);
                }
                else
                {
                    //Date without format specification
                    htmlTemplate = htmlTemplate.Remove(indexItemFound, stringKey.Length + 1);
                    htmlTemplate = htmlTemplate.Insert(indexItemFound, dateTimeValue != null ? dateTimeValue.Value.ToString(TemplateProcessorConstants.TemplateStringDateFormat) : string.Empty);
                }
                indexSearchItem = indexItemFound + 1;
                indexItemFound = htmlTemplate.IndexOf(stringKey, indexSearchItem, StringComparison.Ordinal);
            }
            return htmlTemplate;
        }

        private string GetExtraFieldDefaultValue(ExtraFieldsValues extraFieldValue, string nullStringValue, bool ncalcEncapsulate)
        {
            var stringValue = TemplateProcessorConstants.TemplateExtraFieldDefaultValue;
            var extraFieldType = (ExtraFieldValueTypeEnum)extraFieldValue.ExtraField.Type;

            switch (extraFieldType)
            {
                case ExtraFieldValueTypeEnum.Barcode:
                case ExtraFieldValueTypeEnum.Text:
                    stringValue = string.IsNullOrEmpty(extraFieldValue.StringValue) ? nullStringValue : ncalcEncapsulate ? "'" + extraFieldValue.StringValue + "'" : extraFieldValue.StringValue;
                    break;
                case ExtraFieldValueTypeEnum.Data:
                    stringValue = extraFieldValue.DataValue == null ? nullStringValue : extraFieldValue.DataValue.Value.ToString(TemplateProcessorConstants.TemplateStringDateFormat);
                    break;
                case ExtraFieldValueTypeEnum.Temps:
                    stringValue = extraFieldValue.EnterValue == null ? nullStringValue : TimeSpan.FromMinutes(extraFieldValue.EnterValue.Value).ToString(TemplateProcessorConstants.TemplateStringTimeFormat);
                    break;
                case ExtraFieldValueTypeEnum.Enter:
                    stringValue = extraFieldValue.EnterValue?.ToString(CultureInfo.InvariantCulture) ?? nullStringValue;
                    break;
                case ExtraFieldValueTypeEnum.Decimal:
                    stringValue = GetExtraFieldDefaultDecimalValue(extraFieldValue, nullStringValue);
                    break;
                case ExtraFieldValueTypeEnum.Boolea:
                    stringValue = GetExtraFieldDefaultBoolValue(extraFieldValue);
                    break;
                case ExtraFieldValueTypeEnum.Select:
                    stringValue = GetExtraFieldDefaultSelectorValue(extraFieldValue, nullStringValue, ncalcEncapsulate);
                    break;
                case ExtraFieldValueTypeEnum.Instalation:
                    stringValue = GetExtraFieldDefaultInstallationValue(extraFieldValue, nullStringValue);
                    break;
            }
            return stringValue;
        }

        private string GetExtraFieldDefaultInstallationValue(ExtraFieldsValues extraFieldValue, string nullStringValue)
        {
            string stringValue;
            if (extraFieldValue.MaterialForm != null && extraFieldValue.MaterialForm.Any())
            {
                var materialString = string.Empty;
                foreach (var material in extraFieldValue.MaterialForm)
                {
                    materialString += material.Units + "x - " + material.Description + TemplateProcessorConstants.TemplateExtraFieldSelectorLineBreak;
                    materialString += material.SerialNumber + " - " + material.Reference + TemplateProcessorConstants.TemplateExtraFieldSelectorLineBreak + TemplateProcessorConstants.TemplateExtraFieldSelectorLineBreak;
                }

                stringValue = materialString;
            }
            else
            {
                stringValue = nullStringValue;
            }

            return stringValue;
        }

        private string GetExtraFieldDefaultSelectorValue(ExtraFieldsValues extraFieldValue, string nullStringValue, bool ncalcEncapsulate)
        {
            string stringValue;
            var selectValues = (extraFieldValue.StringValue ?? string.Empty)
                .Split(new[] {TemplateProcessorConstants.TemplateExtraFieldSelectorSeparator}, StringSplitOptions.None)
                .ToList();
            var allowed = extraFieldValue.ExtraField.AllowedStringValues.Split( new[] {TemplateProcessorConstants.TemplateExtraFieldSelectorSeparator}, StringSplitOptions.None);
            if (selectValues.Count == 0 || selectValues.Count == 1 && !allowed.Contains(selectValues[0]))
            {
                stringValue = nullStringValue;
            }
            else
            {
                var s = string.Join(TemplateProcessorConstants.TemplateExtraFieldSelectorSeparator, selectValues.Select(GetSelectValue));
                stringValue = ncalcEncapsulate ? "'" + s + "'" : s;
            }

            return stringValue;
        }

        private string GetExtraFieldDefaultBoolValue(ExtraFieldsValues extraFieldValue)
        {
            string stringValue;
            if (extraFieldValue.BooleaValue.HasValue && extraFieldValue.BooleaValue.Value)
            {
                stringValue = TemplateProcessorConstants.TemplateExtraFieldTrueValue;
            }
            else
            {
                stringValue = TemplateProcessorConstants.TemplateExtraFieldFalseValue;
            }

            return stringValue;
        }

        private string GetExtraFieldDefaultDecimalValue(ExtraFieldsValues extraFieldValue, string nullStringValue)
        {
            string stringValue;
            if (extraFieldValue.DecimalValue == null)
            {
                stringValue = nullStringValue;
            }
            else
            {
                var minDiffValue = 0.00000000001;
                stringValue = Math.Abs(extraFieldValue.DecimalValue.Value - double.MinValue) < minDiffValue
                    ? nullStringValue
                    : extraFieldValue.DecimalValue.Value.ToString(CultureInfo.InvariantCulture);
            }

            return stringValue;
        }

        public string GetSelectValue(string value)
        {
            if (value == null) return string.Empty;
            var startIdPos = value.IndexOf("[", StringComparison.CurrentCulture);
            var endIdPos = value.IndexOf("]", StringComparison.CurrentCulture);
            if (startIdPos >= 0 && endIdPos > startIdPos)
            {
                return value.Substring(endIdPos + 1).Trim();
            }
            return value.Trim();
        }

        public string GetHtmlContent(List<Tuple<string, string>> content)
        {
            var result = string.Empty;
            foreach (var line in content)
            {
                result += "<p>" + line.Item1 + ": " + NewLineString(line.Item2) + "</p>";
            }
            return result;
        }

        private string NewLineString(string str)
        {
            var result = !string.IsNullOrEmpty(str) ? str.Replace("\n", "<br/>") : str;
            return result;
        }
    }
}
