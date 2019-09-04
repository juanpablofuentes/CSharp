using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderStatus;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class WorkOrderStatusListDtoExtensions
    {
        public static WorkOrderStatusListDto ToWorkOrderStatusListDto(this WorkOrderStatusesTranslations translation, WorkOrderStatuses source)
        {
            WorkOrderStatusListDto result = null;
            if (source != null)
            {
                result = new WorkOrderStatusListDto()
                {
                    Id = source.Id,
                    Name = translation?.NameText ?? source.Name,
                    Description = translation?.DescriptionText ?? source.Description,
                };
            }
            return result;
        }

        public static WorkOrderStatusesTranslations FilterByLanguage(this WorkOrderStatuses source, int languageId)
        {
            return source.WorkOrderStatusesTranslations.FirstOrDefault(t => t.LanguageId == languageId);
        }

        public static WorkOrderStatusListDto ToDto(this WorkOrderStatuses source)
        {
            WorkOrderStatusListDto result = null;
            if (source != null)
            {
                result = new WorkOrderStatusListDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    Color = source.Color,
                    Translations = source.WorkOrderStatusesTranslations?.ToList().ToTranslationDto(),
                    IsWorkOrderClosed = source.IsWoclosed ?? false,
                    IsPlannable = source.IsPlannable ?? false,
                };
            }
            return result;
        }

        public static WorkOrderStatuses ToEntity(this WorkOrderStatusListDto source)
        {
            WorkOrderStatuses result = null;
            if (source != null)
            {
                result = new WorkOrderStatuses()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    Color = source.Color,
                    WorkOrderStatusesTranslations = source.Translations.ToWorkOrderStatusTranslationEntity(),
                    IsWoclosed = source.IsWorkOrderClosed,
                    IsPlannable = source.IsPlannable,
                };
            }
            return result;
        }
    }
}