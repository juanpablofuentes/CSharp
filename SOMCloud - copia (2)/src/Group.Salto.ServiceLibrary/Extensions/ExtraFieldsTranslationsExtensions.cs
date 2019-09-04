using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant.ContentTranslations;
using Group.Salto.ServiceLibrary.Common.Dtos.Translation;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ExtraFieldsTranslationsExtensions
    {
        public static ContentTranslationDto ToTranslationDto(this ExtraFieldsTranslations source)
        {
            ContentTranslationDto result = null;
            if (source != null)
            {
                result = new ContentTranslationDto()
                {
                    Id = source.Id,
                    Description = source.DescriptionText,
                    Text = source.NameText,
                    LanguageId = source.LanguageId,
                };
            }

            return result;
        }

        public static IList<ContentTranslationDto> ToTranslationDto(this IList<ExtraFieldsTranslations> source)
        {
            return source?.MapList(x => x.ToTranslationDto());
        }

        public static ExtraFieldsTranslations ToExtraFieldsTranslationEntity(this ContentTranslationDto source)
        {
            ExtraFieldsTranslations result = null;
            if (source != null)
            {
                result = new ExtraFieldsTranslations()
                {
                    Id = source.Id,
                    DescriptionText = source.Description,
                    NameText = source.Text,
                    LanguageId = source.LanguageId,
                    UpdateDate = DateTime.UtcNow,
                };
            }

            return result;
        }

        public static IList<ExtraFieldsTranslations> ToExtraFieldsTranslationEntity(this IList<ContentTranslationDto> source)
        {
            return source?.MapList(x => x.ToExtraFieldsTranslationEntity());
        }
    }
}