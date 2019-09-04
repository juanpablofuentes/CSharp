using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.CollectionsExtraField;
using Group.Salto.ServiceLibrary.Common.Dtos.CollectionsExtraFieldExtraField;
using Group.Salto.ServiceLibrary.Common.Dtos.ExtraFieldTypes;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class CollectionsExtraFieldDetailDtoExtensions
    {
        public static CollectionsExtraFieldDetailDto ToDetailDto(this CollectionsExtraField source, IList<ExtraFieldsTypesDto> extraFieldsTypesDto)
        {
            CollectionsExtraFieldDetailDto result = null;
            if (source != null)
            {
                result = new CollectionsExtraFieldDetailDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    CollectionsExtraFieldExtraField = source.CollectionsExtraFieldExtraField?.ToList()?.ToCollectionsExtraFieldExtraFieldDto(),
                    ExtraFieldsTypes = extraFieldsTypesDto,
                };
            }
            return result;
        }

        public static CollectionsExtraField ToEntity(this CollectionsExtraFieldDetailDto source)
        {
            CollectionsExtraField result = null;
            if (source != null)
            {
                result = new CollectionsExtraField()
                {
                    Name = source.Name,
                    Description = source.Description
                };
            }
            return result;
        }

        public static void UpdateCollectionsExtraField(this CollectionsExtraField target, CollectionsExtraField source)
        {
            if (source != null && target != null)
            {
                target.Name = source.Name;
                target.Description = source.Description;
            }
        }

        public static CollectionsExtraField AssignToAddCollectionsExtraFieldExtraField(this CollectionsExtraField entity, CollectionsExtraFieldDetailDto model, IList<ExtraFields> extraFields)
        {
            IList<CollectionsExtraFieldExtraFieldDto> forInsert = model.CollectionsExtraFieldExtraField.Where(x => x.ExtraField.State == "C").ToList();
            if (forInsert != null && forInsert.Any())
            {
                entity.CollectionsExtraFieldExtraField = entity.CollectionsExtraFieldExtraField ?? new List<CollectionsExtraFieldExtraField>();
                IList<CollectionsExtraFieldExtraField> localEntity = forInsert.ToEntity(extraFields);
                foreach (CollectionsExtraFieldExtraField row in localEntity)
                {
                    entity.CollectionsExtraFieldExtraField.Add(row);
                }
            }

            return entity;
        }

        public static CollectionsExtraField AssignToUpdateCollectionsExtraFieldExtraField(this CollectionsExtraField entity, CollectionsExtraFieldDetailDto model, IList<ExtraFields> extraFields)
        {
            IList<CollectionsExtraFieldExtraFieldDto> forUpdate = model.CollectionsExtraFieldExtraField.Where(x => x.ExtraField.State == "U").ToList();
            if (forUpdate != null && forUpdate.Any())
            {
                foreach (CollectionsExtraFieldExtraField row in entity.CollectionsExtraFieldExtraField)
                {
                    CollectionsExtraFieldExtraFieldDto localExtraField = forUpdate.Where(x => x.ExtraField.Id == row.ExtraFieldId).FirstOrDefault();
                    if (localExtraField != null)
                    {
                        row.UpdateCollectionsExtraFieldExtraField(localExtraField.ToEntityForUpdate(extraFields));
                    }
                }
            }
            return entity;
        }
    }
}