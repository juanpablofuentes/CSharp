using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Postconditions;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common.Helpers;
using Group.Salto.Common.Enums;
using System;
using Group.Salto.ServiceLibrary.Common.Dtos.PostconditionsTypes;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class PostconditionsDtoExtensions
    {
        public static PostconditionsDto ToDto(this Postconditions source, List<PostconditionsTypesDto> types)
        {
            var postconditionType = types.Where(x => x.Id == source.PostconditionsTypeId).FirstOrDefault();
            PostconditionsDto result = null;
            if (source != null)
            {
                int typeId = 0;
                string typeName = "";

                bool? booleanValue = null;
                string stringValue = "";
                double? decimalValue = null;
                DateTime? dateValue = null;
                int? enterValue = null;

                switch (source.NameFieldModel)
                {
                    case nameof(PostconditionsTypeEnum.Billable):
                        booleanValue = source.BooleanValue;
                        break;

                    case nameof(PostconditionsTypeEnum.Cua):
                        typeName = source.Queue?.Name ?? string.Empty;
                        typeId = source.QueueId.HasValue ? source.QueueId.Value : 0;
                        break;

                    case nameof(PostconditionsTypeEnum.DataRecollida):
                    case nameof(PostconditionsTypeEnum.DataAssignacio):
                    case nameof(PostconditionsTypeEnum.DataActuacio):
                    case nameof(PostconditionsTypeEnum.DataTancamentSalto):
                    case nameof(PostconditionsTypeEnum.DataTancamentClient):
                    case nameof(PostconditionsTypeEnum.WOReopeningPolicy):
                    case nameof(PostconditionsTypeEnum.ActuationEndDate):
                    case nameof(PostconditionsTypeEnum.DataResolucio):
                        enterValue = source.EnterValue;
                        break;

                    case nameof(PostconditionsTypeEnum.Tecnic):
                        typeName = source.PeopleTechnicians?.Name ?? string.Empty;
                        typeId = source.PeopleTechniciansId.HasValue ? source.PeopleTechniciansId.Value : 0;
                        break;

                    case nameof(PostconditionsTypeEnum.EstatOTExtern):
                        typeName = source.ExternalWorOrderStatus?.Name ?? string.Empty;
                        typeId = source.ExternalWorOrderStatusId.HasValue ? source.ExternalWorOrderStatusId.Value : 0;
                        break;

                    case nameof(PostconditionsTypeEnum.Manipulador):
                        typeId = source.PeopleManipulatorId.HasValue ? source.PeopleManipulatorId.Value : 0;
                        break;

                    case nameof(PostconditionsTypeEnum.EstatOT):
                    case nameof(PostconditionsTypeEnum.ParentWOInternalStatus):
                    case nameof(PostconditionsTypeEnum.ParentWOExternalStatus):
                        typeName = source.WorkOrderStatus?.Name ?? string.Empty;
                        typeId = source.WorkOrderStatusId.HasValue ? source.WorkOrderStatusId.Value : 0;
                        break;

                    case nameof(PostconditionsTypeEnum.ParentWOQueue):
                        typeName = source.Queue?.Name ?? string.Empty;
                        typeId = source.QueueId.HasValue ? source.QueueId.Value : 0;
                        break;

                    case nameof(PostconditionsTypeEnum.ObservacionsOT):
                        stringValue = source.StringValue;
                        break;

                    case nameof(PostconditionsTypeEnum.TipusOTN1):
                        typeName = "";
                        typeId = 0;
                        booleanValue = null;
                        stringValue = "";
                        decimalValue = null;
                        dateValue = null;
                        enterValue = null;
                        break;

                    default:
                        typeName = "TODOExtension";
                        break;
                }    
                result = new PostconditionsDto()
                {
                    Id = source.Id,
                    PostconditionCollectionsId = source.PostconditionCollectionsId,
                    NameFieldModel = source.NameFieldModel,
                    TypeId = typeId,
                    TypeName = typeName,
                    BooleanValue = booleanValue,
                    DecimalValue = decimalValue,
                    StringValue = stringValue,
                    DateValue = dateValue,
                    EnterValue = enterValue,
                    PostconditionTypeId = postconditionType.Id,
                    PostconditionTypeName = postconditionType.Name,
                };
            }
            return result;
        }

        public static Postconditions ToEntity(this Postconditions local, PostconditionsDto source, PostconditionsTypesDto type)
        {
            if (source != null)
            {
                if (local == null)
                {
                    local = new Postconditions()
                    {
                        NameFieldModel = type.Description,
                        PostconditionsTypeId = type.Id,
                        PostconditionCollectionsId = source.PostconditionCollectionsId,
                    };
                }
                else
                {
                    local.NameFieldModel = type.Description;
                    local.PostconditionsTypeId = type.Id;
                }
                switch (type.Description)
                {
                    case nameof(PostconditionsTypeEnum.Billable):
                        local.BooleanValue = source.BooleanValue;
                        break;

                    case nameof(PostconditionsTypeEnum.Cua):
                        local.QueueId = source.TypeId;
                        break;

                    case nameof(PostconditionsTypeEnum.DataRecollida):
                    case nameof(PostconditionsTypeEnum.DataAssignacio):
                    case nameof(PostconditionsTypeEnum.DataActuacio):
                    case nameof(PostconditionsTypeEnum.DataTancamentSalto):
                    case nameof(PostconditionsTypeEnum.DataTancamentClient):
                    case nameof(PostconditionsTypeEnum.WOReopeningPolicy):
                    case nameof(PostconditionsTypeEnum.ActuationEndDate):
                    case nameof(PostconditionsTypeEnum.DataResolucio):
                        local.EnterValue = source.EnterValue;

                        break;

                    case nameof(PostconditionsTypeEnum.Tecnic):
                        local.PeopleTechniciansId = source.TypeId;
                        break;

                    case nameof(PostconditionsTypeEnum.EstatOTExtern):
                        local.ExternalWorOrderStatusId = source.TypeId;
                        break;

                    case nameof(PostconditionsTypeEnum.Manipulador):
                        local.PeopleManipulatorId = source.TypeId == 0 ? (int?) null : source.TypeId;
                        break;

                    case nameof(PostconditionsTypeEnum.EstatOT):
                    case nameof(PostconditionsTypeEnum.ParentWOInternalStatus):
                    case nameof(PostconditionsTypeEnum.ParentWOExternalStatus):
                        local.WorkOrderStatusId = source.TypeId;
                        break;

                    case nameof(PostconditionsTypeEnum.ParentWOQueue):
                        local.QueueId = source.TypeId;
                        break;

                    case nameof(PostconditionsTypeEnum.ObservacionsOT):
                        local.StringValue = source.StringValue;
                        break;
                }
            }
            return local;
        }

        public static IList<PostconditionsDto> ToDto(this IList<Postconditions> source, List<PostconditionsTypesDto> types)
        {
            return source?.MapList(x => x.ToDto(types)).ToList();
        }
    }
}