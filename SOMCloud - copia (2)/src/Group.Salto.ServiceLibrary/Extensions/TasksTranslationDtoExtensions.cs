using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant.ContentTranslations;
using Group.Salto.ServiceLibrary.Common.Dtos.TasksTranslations;
using Group.Salto.ServiceLibrary.Common.Dtos.Translation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class TasksTranslationDtoExtensions
    {
        public static TasksTranslationsDto ToDto(this TasksTranslations source)
        {
            TasksTranslationsDto result = new TasksTranslationsDto
            {
                Id = source.Id,
                NameText = source.NameText,
                DescriptionText = source.DescriptionText,
                LanguageId = source.LanguageId
            };
            return result;
        }

        public static IList<TasksTranslationsDto> ToDto(this ICollection<TasksTranslations> source)
        {
            return source?.MapList(x => x.ToDto()).ToList();
        }

        public static ContentTranslationDto ToTranslationDto(this TasksTranslations source)
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

        public static IList<ContentTranslationDto> ToTranslationDto(this IList<TasksTranslations> source)
        {
            return source?.MapList(x => x.ToTranslationDto());
        }

        public static TasksTranslations ToTasksTranslationEntity(this ContentTranslationDto source)
        {
            TasksTranslations result = null;
            if (source != null)
            {
                result = new TasksTranslations()
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