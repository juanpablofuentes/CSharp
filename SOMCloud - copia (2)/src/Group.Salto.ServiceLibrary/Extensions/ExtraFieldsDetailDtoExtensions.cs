using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.Entities.Tenant.ContentTranslations;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.ExtraFields;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ExtraFieldsDetailDtoExtensions
    {
        public static ExtraFieldsDetailDto ToExtraFieldsListDto(this ExtraFieldsTranslations translation, IList<BaseNameIdDto<int>> types, ExtraFields source)
        {
            ExtraFieldsDetailDto result = null;
            if (source != null)
            {
                result = new ExtraFieldsDetailDto()
                {
                    Id = source.Id,
                    Name = translation?.NameText??source.Name,
                    Description = translation?.DescriptionText??source.Description,
                    TypeId = source.Type,
                    TypeName = types.FirstOrDefault(x => x.Id == source.Type).Name,
                    ErpSystemInstanceQueryId = source.ErpSystemInstanceQueryId,
                    AllowedStringValues = source.AllowedStringValues,
                    MultipleChoice = source.MultipleChoice,
                    IsMandatory = source.IsMandatory,
                    DelSystem = source.DelSystem,
                    Translations = source.ExtraFieldsTranslations?.ToList().ToTranslationDto(),
                };
            }
            return result;
        }

        public static ExtraFieldsTranslations FilterByLanguage(this ExtraFields source, int languageId)
        {
            return source.ExtraFieldsTranslations.FirstOrDefault(t => t.LanguageId == languageId);
        }

        public static ExtraFieldsDetailDto ToDetailDto(this ExtraFields source)
        {
            ExtraFieldsDetailDto result = null;
            if (source != null)
            {
                result = new ExtraFieldsDetailDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    TypeId = source.Type,
                    ErpSystemInstanceQueryId = source.ErpSystemInstanceQueryId,
                    AllowedStringValues = source.AllowedStringValues,
                    MultipleChoice = source.MultipleChoice,
                    IsMandatory = source.IsMandatory,
                    DelSystem = source.DelSystem,
                    Translations = source.ExtraFieldsTranslations?.ToList().ToTranslationDto(),
                };
            }
            return result;
        }

        public static IList<ExtraFieldsDetailDto> ToListDetailDto(this IList<ExtraFields> source)
        {
            return source?.MapList(x => x.ToDetailDto());
        }

        public static ExtraFields ToEntity(this ExtraFieldsDetailDto source)
        {
            ExtraFields result = null;
            if (source != null)
            {
                result = new ExtraFields()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    IsMandatory = source.IsMandatory,
                    Type = source.TypeId,
                    ErpSystemInstanceQueryId = source.ErpSystemInstanceQueryId,
                    MultipleChoice = source.MultipleChoice,
                    AllowedStringValues = source.AllowedStringValues,
                    DelSystem = source.DelSystem.HasValue ? source.DelSystem.Value : false,
                    ExtraFieldsTranslations = source.Translations.ToExtraFieldsTranslationEntity(),
                };
            }
            return result;
        }
    }
}