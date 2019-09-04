using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.PreconditionLiteralValues;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class PreconditionLiteralValuesDtoExtension
    {
        public static PreconditionLiteralValuesDto ToDto(this PreconditionsLiteralValues source, string type)
        {
            PreconditionLiteralValuesDto result = null;
            if (source != null)
            {
                int typeId = 0;
                string typeName = "";

                bool? booleanValue = null;
                string stringValue = "";
                double? decimalValue = null;
                DateTime? dateValue = null;
                int? enterValue = null;

                switch (type)
                {
                    case nameof(PreconditionFieldNameEnum.Billable):
                        booleanValue = source.BooleanValue;
                        break;

                    case nameof(PreconditionFieldNameEnum.ClientFinal):
                        typeName = source.FinalClient?.Name ?? string.Empty;
                        typeId = source.FinalClientId.HasValue? source.FinalClientId.Value: 0;
                        break;

                    case nameof(PreconditionFieldNameEnum.Cua):
                        typeName = source.Queue?.Name ?? string.Empty;
                        typeId = source.QueueId.HasValue ? source.QueueId.Value : 0;
                        break;

                    case nameof(PreconditionFieldNameEnum.Equip):
                        typeName = source.Asset?.SerialNumber ?? source.Asset?.Observations;
                        typeId = source.AssetId.HasValue ? source.AssetId.Value : 0;
                        break;

                    case nameof(PreconditionFieldNameEnum.EstatOT):
                        typeName = source.WorkOrderStatus?.Name ?? string.Empty;
                        typeId = source.WorkOrderStatusId.HasValue ? source.WorkOrderStatusId.Value : 0;
                        break;

                    case nameof(PreconditionFieldNameEnum.EstatOTExtern):
                        typeName = source.ExternalWorOrderStatus?.Name ?? string.Empty;
                        typeId = source.ExternalWorOrderStatusId.HasValue ? source.ExternalWorOrderStatusId.Value : 0;
                        break;

                    case nameof(PreconditionFieldNameEnum.Manipulador):
                        typeName = source.PeopleManipulator?.Name ?? string.Empty;
                        typeId = source.PeopleManipulatorId.HasValue ? source.PeopleManipulatorId.Value : 0;
                        break;

                    case nameof(PreconditionFieldNameEnum.TipusOTN1):
                        typeName = source.WorkOrderTypesN1?.Name ?? string.Empty;
                        typeId = source.WorkOrderTypesN1id.HasValue ? source.WorkOrderTypesN1id.Value : 0;
                        break;

                    case nameof(PreconditionFieldNameEnum.TipusOTN2):
                        typeName = source.WorkOrderTypesN2?.Name ?? string.Empty;
                        typeId = source.WorkOrderTypesN2id.HasValue ? source.WorkOrderTypesN2id.Value : 0;
                        break;

                    case nameof(PreconditionFieldNameEnum.TipusOTN3):
                        typeName = source.WorkOrderTypesN3?.Name ?? string.Empty;
                        typeId = source.WorkOrderTypesN3id.HasValue ? source.WorkOrderTypesN3id.Value : 0;
                        break;

                    case nameof(PreconditionFieldNameEnum.TipusOTN4):
                        typeName = source.WorkOrderTypesN4?.Name ?? string.Empty;
                        typeId = source.WorkOrderTypesN4id.HasValue ? source.WorkOrderTypesN4id.Value : 0;
                        break;

                    case nameof(PreconditionFieldNameEnum.TipusOTN5):
                        typeName = source.WorkOrderTypesN5?.Name ?? string.Empty;
                        typeId = source.WorkOrderTypesN5id.HasValue ? source.WorkOrderTypesN5id.Value : 0;
                        break;

                    case nameof(PreconditionFieldNameEnum.Project):
                        typeName = source.Project?.Name ?? string.Empty;
                        typeId = source.ProjectId.HasValue ? source.ProjectId.Value : 0;
                        break;

                    case nameof(PreconditionFieldNameEnum.WOCategory):
                        typeName = source.WorkOrderCategory?.Name ?? string.Empty;
                        typeId = source.WorkOrderCategoryId.HasValue ? source.WorkOrderCategoryId.Value : 0;
                        break;

                    case nameof(PreconditionFieldNameEnum.State):
                        typeName = source.Location?.Name ?? string.Empty;
                        typeId = source.RegionId.HasValue ? source.RegionId.Value : 0;
                        break;

                    case nameof(PreconditionFieldNameEnum.UbicacioClientFinal):
                        typeName = source.Location?.Name ?? string.Empty;
                        typeId = source.LocationId.HasValue ? source.LocationId.Value : 0;
                        break;

                    case nameof(PreconditionFieldNameEnum.Zone):
                        typeName = source.Zone?.Name ?? string.Empty;
                        typeId = source.ZoneId.HasValue ? source.ZoneId.Value : 0;
                        break;

                    case nameof(PreconditionFieldNameEnum.Tecnic):
                        typeName = source.PeopleTechnician?.Name ?? string.Empty;
                        typeId = source.PeopleTechnicianId.HasValue ? source.PeopleTechnicianId.Value : 0;
                        break;

                    case nameof(PreconditionFieldNameEnum.DataActuacio):
                    case nameof(PreconditionFieldNameEnum.DataAssignacio):
                    case nameof(PreconditionFieldNameEnum.DataCreacio):
                    case nameof(PreconditionFieldNameEnum.DataRecollida):
                    case nameof(PreconditionFieldNameEnum.DataTancamentClient):
                    case nameof(PreconditionFieldNameEnum.DataTancamentSalto):
                        enterValue = source.EnterValue;
                        break;

                    default:
                        typeName = "TODOExtension";
                        break;
                }

                result = new PreconditionLiteralValuesDto()
                {
                    Id = source.Id,
                    LiteralPreconditionId = source.LiteralPreconditionId.HasValue? source.LiteralPreconditionId.Value : 0,
                    TypeId = typeId,
                    TypeName = typeName,
                    BooleanValue = booleanValue,
                    DecimalValue = decimalValue,
                    StringValue = stringValue,
                    DateValue = dateValue,
                    EnterValue = enterValue,
                };

            }

            return result;
        }

        public static PreconditionsLiteralValues ToEntity(this PreconditionLiteralValuesDto source, string type)
        {
            PreconditionsLiteralValues result = null;
            if (source != null)
            {
                result = new PreconditionsLiteralValues();
                result.Id = source.Id;
                result.LiteralPreconditionId = source.LiteralPreconditionId;
                switch (type)
                {
                    case nameof(PreconditionFieldNameEnum.Billable):
                        result.BooleanValue = source.BooleanValue;
                        break;

                    case nameof(PreconditionFieldNameEnum.ClientFinal):
                        result.FinalClientId = source.TypeId;
                        break;

                    case nameof(PreconditionFieldNameEnum.Cua):
                        result.QueueId = source.TypeId;
                        break;

                    case nameof(PreconditionFieldNameEnum.Equip):
                        result.AssetId = source.TypeId;
                        break;

                    case nameof(PreconditionFieldNameEnum.EstatOT):
                        result.WorkOrderStatusId = source.TypeId;
                        break;

                    case nameof(PreconditionFieldNameEnum.EstatOTExtern):
                        result.ExternalWorOrderStatusId = source.TypeId;
                        break;

                    case nameof(PreconditionFieldNameEnum.Manipulador):
                        result.PeopleManipulatorId = source.TypeId;
                        break;

                    case nameof(PreconditionFieldNameEnum.TipusOTN1):
                        result.WorkOrderTypesN1id = source.TypeId;
                        break;

                    case nameof(PreconditionFieldNameEnum.TipusOTN2):
                        result.WorkOrderTypesN2id = source.TypeId;
                        break;

                    case nameof(PreconditionFieldNameEnum.TipusOTN3):
                        result.WorkOrderTypesN3id = source.TypeId;
                        break;

                    case nameof(PreconditionFieldNameEnum.TipusOTN4):
                        result.WorkOrderTypesN4id = source.TypeId;
                        break;

                    case nameof(PreconditionFieldNameEnum.TipusOTN5):
                        result.WorkOrderTypesN5id = source.TypeId;
                        break;

                    case nameof(PreconditionFieldNameEnum.Project):
                        result.ProjectId = source.TypeId;
                        break;

                    case nameof(PreconditionFieldNameEnum.WOCategory):
                        result.WorkOrderCategoryId = source.TypeId;
                        break;

                    case nameof(PreconditionFieldNameEnum.State):
                        result.RegionId = source.TypeId;
                        break;

                    case nameof(PreconditionFieldNameEnum.UbicacioClientFinal):
                        result.LocationId = source.TypeId;
                        break;

                    case nameof(PreconditionFieldNameEnum.Zone):
                        result.ZoneId = source.TypeId;
                        break;

                    case nameof(PreconditionFieldNameEnum.Tecnic):
                        result.PeopleTechnicianId = source.TypeId;
                        break;

                    case nameof(PreconditionFieldNameEnum.DataActuacio):
                        result.EnterValue = source.EnterValue;
                        break;

                    case nameof(PreconditionFieldNameEnum.DataAssignacio):
                        result.EnterValue = source.EnterValue;
                        break;

                    case nameof(PreconditionFieldNameEnum.DataCreacio):
                        result.EnterValue = source.EnterValue;
                        break;

                    case nameof(PreconditionFieldNameEnum.DataRecollida):
                        result.EnterValue = source.EnterValue;
                        break;

                    case nameof(PreconditionFieldNameEnum.DataTancamentClient):
                        result.EnterValue = source.EnterValue;
                        break;

                    case nameof(PreconditionFieldNameEnum.DataTancamentSalto):
                        result.EnterValue = source.EnterValue;
                        break;

                    default:
                        break;
                }
            }
            return result;
        }

        public static IList<PreconditionsLiteralValues> ToEntity(this IList<PreconditionLiteralValuesDto> source, string type)
        {
            return source?.MapList(e => e.ToEntity(type));
        }

        public static IList<PreconditionLiteralValuesDto> ToDto(this ICollection<PreconditionsLiteralValues> source, string type)
        {
            return source?.MapList(x => x.ToDto(type)).ToList();
        }

        public static IList<PreconditionLiteralValuesDto> ToDto(this IQueryable<PreconditionsLiteralValues> source, string type)
        {
            return source?.MapList(x => x.ToDto(type)).ToList();
        }
    }
}