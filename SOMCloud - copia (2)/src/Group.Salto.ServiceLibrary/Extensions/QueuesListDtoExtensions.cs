using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Queue;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class QueueListDtoExtensions
    {
        public static QueueListDto ToQueueListDto(this QueuesTranslations translation, Queues source)
        {
            QueueListDto result = null;
            if (source != null)
            {
                result = new QueueListDto()
                {
                    Id = source.Id,
                    Name = translation?.NameText ?? source.Name,
                    Description = translation?.DescriptionText ?? source.Description,
                };
            }
            return result;
        }

        public static QueuesTranslations FilterByLanguage(this Queues source, int languageId)
        {
            return source.QueuesTranslations.FirstOrDefault(t => t.LanguageId == languageId);
        }

        public static QueueListDto ToDto(this Queues source)
        {
            QueueListDto result = null;
            if (source != null)
            {
                result = new QueueListDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    Translations = source.QueuesTranslations?.ToList().ToTranslationDto(),
                    PermissionsSelected = source.PermissionQueue?.Select(x=>x.PermissionId)?.ToList(),
                };
            }
            return result;
        }

        public static Queues ToEntity(this QueueListDto source)
        {
            Queues result = null;
            if (source != null)
            {
                result = new Queues()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    QueuesTranslations = source.Translations.ToQueueTranslationEntity(),
                };
            }
            return result;
        }
    }
}