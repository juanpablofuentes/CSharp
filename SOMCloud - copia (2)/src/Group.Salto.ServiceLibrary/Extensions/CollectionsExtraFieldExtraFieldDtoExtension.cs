using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.CollectionsExtraFieldExtraField;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class CollectionsExtraFieldExtraFieldDtoExtension
    {
        public static CollectionsExtraFieldExtraFieldDto ToCollectionsExtraFieldExtraFieldDto(this CollectionsExtraFieldExtraField source)
        {
            CollectionsExtraFieldExtraFieldDto result = null;
            if (source != null)
            {
                result = new CollectionsExtraFieldExtraFieldDto()
                {
                    ExtraFieldId = source.ExtraFieldId,
                    Position = source.Position,
                    ExtraField = source.ExtraField?.ToListDto(),
                };
            }
            return result;
        }

        public static IList<CollectionsExtraFieldExtraFieldDto> ToCollectionsExtraFieldExtraFieldDto(this IList<CollectionsExtraFieldExtraField> source)
        {
            return source?.OrderBy(x => x.Position).ToList().MapList(cc => cc.ToCollectionsExtraFieldExtraFieldDto());
        }

        public static CollectionsExtraFieldExtraField ToEntityForUpdate(this CollectionsExtraFieldExtraFieldDto source, IList<ExtraFields> extraFields)
        {
            CollectionsExtraFieldExtraField result = null;
            if (source != null)
            {
                result = new CollectionsExtraFieldExtraField()
                {
                    Position = source.Position,
                    ExtraFieldId = source.ExtraFieldId
                };

                var existExtraField = extraFields.Any(x => x.Id == source.ExtraFieldId);
                if (existExtraField)
                {
                    result.ExtraField = new ExtraFields()
                    {
                        Description = source.ExtraField.Description,
                        IsMandatory = source.ExtraField.IsMandatory,
                        MultipleChoice = source.ExtraField.MultipleChoice,
                        AllowedStringValues = source.ExtraField.AllowedStringValues,
                        ErpSystemInstanceQueryId = source.ExtraField.ErpSystemInstanceQueryId,
                        UpdateDate = DateTime.UtcNow
                    };
                }
            }

            return result;
        }

        public static CollectionsExtraFieldExtraField ToEntity(this CollectionsExtraFieldExtraFieldDto source, IList<ExtraFields> extraFields)
        {
            CollectionsExtraFieldExtraField result = null;
            if (source != null)
            {
                result = new CollectionsExtraFieldExtraField()
                {
                    Position = source.Position,
                    ExtraFieldId = source.ExtraFieldId
                };

                var existExtraField = extraFields.Any(x => x.Id == source.ExtraFieldId);
                if (!existExtraField)
                {
                    result.ExtraField = new ExtraFields()
                    {
                        Name = source.ExtraField.Name,
                        Description = source.ExtraField.Description,
                        Type = source.ExtraField.TypeId,
                        IsMandatory = source.ExtraField.IsMandatory,
                        MultipleChoice = source.ExtraField.MultipleChoice,
                        AllowedStringValues = source.ExtraField.AllowedStringValues,
                        ErpSystemInstanceQueryId = source.ExtraField.ErpSystemInstanceQueryId,
                        DelSystem = source.ExtraField.DelSystem.HasValue ? source.ExtraField.DelSystem.Value : false,
                        UpdateDate = DateTime.UtcNow
                    };
                }
            }

            return result;
        }

        public static IList<CollectionsExtraFieldExtraField> ToEntity(this IList<CollectionsExtraFieldExtraFieldDto> source, IList<ExtraFields> extraFields)
        {
            return source?.MapList(e => e.ToEntity(extraFields));
        }

        public static IList<CollectionsExtraFieldExtraField> ToEntityForUpdate(this IList<CollectionsExtraFieldExtraFieldDto> source, IList<ExtraFields> extraFields)
        {
            return source?.MapList(e => e.ToEntityForUpdate(extraFields));
        }

        public static void UpdateCollectionsExtraFieldExtraField(this CollectionsExtraFieldExtraField target, CollectionsExtraFieldExtraField source)
        {
            if (source != null && target != null)
            {
                target.Position = source.Position;
                if (source.ExtraField != null)
                {
                    target.ExtraField.Description = source.ExtraField.Description;
                    target.ExtraField.IsMandatory = source.ExtraField.IsMandatory;
                    target.ExtraField.MultipleChoice = source.ExtraField.MultipleChoice;
                    target.ExtraField.AllowedStringValues = source.ExtraField.AllowedStringValues;
                    target.ExtraField.ErpSystemInstanceQueryId = source.ExtraField.ErpSystemInstanceQueryId;
                    target.ExtraField.UpdateDate = source.ExtraField.UpdateDate;
                }
            }
        }
    }
}