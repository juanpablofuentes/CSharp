using System;
using System.Collections.Generic;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Translation;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class QueueTranslationsExtensions
    {
        public static ContentTranslationDto ToTranslationDto(this QueuesTranslations source)
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

        public static IList<ContentTranslationDto> ToTranslationDto(this IList<QueuesTranslations> source)
        {
            return source?.MapList(x => x.ToTranslationDto());
        }

        public static QueuesTranslations ToQueueTranslationEntity(this ContentTranslationDto source)
        {
            QueuesTranslations result = null;
            if (source != null)
            {
                result = new QueuesTranslations()
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

        public static IList<QueuesTranslations> ToQueueTranslationEntity(this IList<ContentTranslationDto> source)
        {
            return source?.MapList(x => x.ToQueueTranslationEntity());
        }
    }
}