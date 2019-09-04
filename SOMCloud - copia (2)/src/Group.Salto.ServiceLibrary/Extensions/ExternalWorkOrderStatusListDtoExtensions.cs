using System;
using System.Collections.Generic;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderStatus;
using System.Linq;
using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.ExternalWorkOrderStatus;
using Group.Salto.ServiceLibrary.Common.Dtos.Translation;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ExternalWorkOrderStatusListDtoExtensions
    {
        public static ExternalWorkOrderStatusListDto ToWorkOrderStatusListDto(this ExternalWorkOrderStatusesTranslations translation, ExternalWorOrderStatuses source)
        {
            ExternalWorkOrderStatusListDto result = null;
            if (source != null)
            {
                result = new ExternalWorkOrderStatusListDto()
                {
                    Id = source.Id,
                    Name = translation?.NameText ?? source.Name,
                    Description = translation?.DescriptionText ?? source.Description,
                };
            }
            return result;
        }

        public static ExternalWorkOrderStatusesTranslations FilterByLanguage(this ExternalWorOrderStatuses source, int languageId)
        {
            return source.ExternalWorkOrderStatusesTranslations.FirstOrDefault(t => t.LanguageId == languageId);
        }

        public static ExternalWorkOrderStatusListDto ToDto(this ExternalWorOrderStatuses source)
        {
            ExternalWorkOrderStatusListDto result = null;
            if (source != null)
            {
                result = new ExternalWorkOrderStatusListDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    Color = source.Color,
                    Translations = source.ExternalWorkOrderStatusesTranslations?.ToList().ToTranslationDto(),
                  
                };
            }
            return result;
        }

        public static ExternalWorOrderStatuses ToEntity(this ExternalWorkOrderStatusListDto source)
        {
            ExternalWorOrderStatuses result = null;
            if (source != null)
            {
                result = new ExternalWorOrderStatuses()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    Color = source.Color,
                    ExternalWorkOrderStatusesTranslations = source.Translations.ToExternalWorkOrderStatusTranslationEntity(),
                };
            }
            return result;
        }

        public static IList<ContentTranslationDto> ToTranslationDto(this IList<ExternalWorkOrderStatusesTranslations> source)
        {
            return source?.MapList(x => x.ToTranslationDto());
        }

        public static ContentTranslationDto ToTranslationDto(this ExternalWorkOrderStatusesTranslations source)
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

        public static IList<ExternalWorkOrderStatusesTranslations> ToExternalWorkOrderStatusTranslationEntity(this IList<ContentTranslationDto> source)
        {
            return source?.MapList(x => x.ToExternalWorkOrderStatusTranslationEntity());
        }

        public static ExternalWorkOrderStatusesTranslations ToExternalWorkOrderStatusTranslationEntity(this ContentTranslationDto source)
        {
            ExternalWorkOrderStatusesTranslations result = null;
            if (source != null)
            {
                result = new ExternalWorkOrderStatusesTranslations()
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
    }
}