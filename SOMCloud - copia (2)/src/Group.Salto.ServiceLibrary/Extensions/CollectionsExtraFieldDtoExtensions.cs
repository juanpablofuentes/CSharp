using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.CollectionsExtraField;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Service;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class CollectionsExtraFieldDtoExtensions
    {
        public static CollectionsExtraFieldDto ToListDto(this CollectionsExtraField source)
        {
            CollectionsExtraFieldDto result = null;
            if (source != null)
            {
                result = new CollectionsExtraFieldDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                };
            }

            return result;
        }

        public static IList<CollectionsExtraFieldDto> ToListDto(this IList<CollectionsExtraField> source)
        {
            return source?.MapList(x => x.ToListDto());
        }

        public static ExtendedExtraFieldValueDto ToExtendedExtraFieldValueDto(this CollectionsExtraFieldExtraField dbModel)
        {
            var dto = new ExtendedExtraFieldValueDto
            {
                ExtraFieldId = dbModel.ExtraFieldId,
                Position = dbModel.Position,
                Id = dbModel.ExtraField.Id,
                Name = dbModel.ExtraField?.Name,
                Observations = dbModel.ExtraField?.Observations,
                Description = dbModel.ExtraField?.Description,
                TypeId = dbModel.ExtraField?.Type,
                DelSystem = dbModel.ExtraField?.DelSystem ?? false,
                ErpSystemInstanceQueryId = dbModel.ExtraField?.ErpSystemInstanceQueryId,
                AllowedStringValues = dbModel.ExtraField?.AllowedStringValues,
                MultipleChoice = dbModel.ExtraField?.MultipleChoice,
                IsMandatory = dbModel.ExtraField?.IsMandatory
            };

            return dto;
        }

        public static IEnumerable<ExtendedExtraFieldValueDto> ToCollectionsServiceExtraFieldDto(this IEnumerable<CollectionsExtraFieldExtraField> dbModelList)
        {
            var dtoList = new List<ExtendedExtraFieldValueDto>();
            foreach (var dbModel in dbModelList.OrderBy(x => x.Position))
            {
                dtoList.Add(dbModel.ToExtendedExtraFieldValueDto());
            }
            return dtoList;
        }

        public static TaskCollectionsServiceExtraFieldDto ToCollectionsExtraFieldDto(this CollectionsExtraField dbModel)
        {
            var dto = new TaskCollectionsServiceExtraFieldDto
            {
                Id = dbModel.Id,
                Name = dbModel.Name,
                Observations = dbModel.Observations,
                Description = dbModel.Description,
                ExtraFieldValues = dbModel.CollectionsExtraFieldExtraField?.ToCollectionsServiceExtraFieldDto()
            };

            return dto;
        }
    }
}