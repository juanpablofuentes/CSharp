using System;
using System.Collections.Generic;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Translation;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class WorkOrderStatusTranslationsExtensions
    {
        public static ContentTranslationDto ToTranslationDto(this WorkOrderStatusesTranslations source)
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

        public static IList<ContentTranslationDto> ToTranslationDto(this IList<WorkOrderStatusesTranslations> source)
        {
            return source?.MapList(x => x.ToTranslationDto());
        }

        public static WorkOrderStatusesTranslations ToWorkOrderStatusTranslationEntity(this ContentTranslationDto source)
        {
            WorkOrderStatusesTranslations result = null;
            if (source != null)
            {
                result = new WorkOrderStatusesTranslations()
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

        public static IList<WorkOrderStatusesTranslations> ToWorkOrderStatusTranslationEntity(this IList<ContentTranslationDto> source)
        {
            return source?.MapList(x => x.ToWorkOrderStatusTranslationEntity());
        }
    }
}