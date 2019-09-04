using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.Entities.Tenant.ContentTranslations;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Tasks;
using Group.Salto.ServiceLibrary.Common.Dtos.TasksTranslations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class TaskDetailDtoExtensions
    {
        public static TasksDetailDto ToTasksListDto(this TasksTranslationsDto translation, Tasks source)
        {
            TasksDetailDto result = null;
            if (source != null)
            {
                result = new TasksDetailDto()
                {
                    TaskId = source.Id,
                    Name = translation?.NameText ?? source.Name,
                    Description = translation?.DescriptionText ?? source.Description,                   
                    Translations = source.TasksTranslations?.ToList().ToTranslationDto()       
                };
            }
            return result;
        }

        public static TasksDetailDto ToTasksListDto(this TasksTranslations translation, Tasks source)
        {
            TasksDetailDto result = null;
            if (source != null)
            {
                result = new TasksDetailDto()
                {
                    TaskId = source.Id,
                    Name = translation?.NameText ?? source.Name,
                    Description = translation?.DescriptionText ?? source.Description,

                    Translations = source.TasksTranslations?.ToList().ToTranslationDto(),
                };
            }
            return result;
        }

        public static TasksTranslations FilterByLanguage(this Tasks source, int languageId)
        {
            return source.TasksTranslations.FirstOrDefault(t => t.LanguageId == languageId);
        }

        public static TasksDetailDto ToDetailDto(this Tasks source)
        {
            TasksDetailDto result = null;
            if (source != null)
            {
                List<int> selectedPermissions = new List<int>();
                if (source.PermissionsTasks.Any()){                    
                    foreach (PermissionsTasks permission in source.PermissionsTasks)
                    {
                        selectedPermissions.Add(permission.PermissionId);
                    }
                }
                result = new TasksDetailDto()
                {
                    TaskId = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    Translations = source.TasksTranslations?.ToList().ToTranslationDto(),
                    TasksPlainTranslations = source.TasksTranslations?.ToList().ToDto(), 
                    PermissionsTasksSelected = selectedPermissions,
                    TriggerTypesId = source.TriggerTypesId
                };
            }
            return result;
        }

        public static IList<TasksDetailDto> ToListDetailDto(this IList<Tasks> source)
        {
            return source?.MapList(x => x.ToDetailDto());
        }

        public static Tasks ToEntity(this TasksDetailDto source)
        {
            Tasks result = null;
            if (source != null)
            {
                Guid triggerGuid = new Guid();
                if (source.TriggerTypesId != null && source.TriggerTypesId != triggerGuid)
                {
                    triggerGuid = (Guid)source.TriggerTypesId;
                }
                result = new Tasks()
                {
                    Id = source.TaskId,
                    Name = source.Name,
                    Description = source.Description,
                    TasksTranslations = source.TasksPlainTranslations?.ToTasksTranslationEntity(),
                    TriggerTypesId = triggerGuid
                };
            }
            return result;
        }

        public static TasksTranslations ToTasksTranslationEntity(this TasksTranslationsDto source)
        {
            TasksTranslations result = null;
            if (source != null)
            {
                result = new TasksTranslations()
                {
                    Id = source.Id,
                    DescriptionText = source.DescriptionText,
                    NameText = source.NameText,
                    LanguageId = source.LanguageId,
                    UpdateDate = DateTime.UtcNow,
                };
            }

            return result;
        }

        public static IList<TasksTranslations> ToTasksTranslationEntity(this IList<TasksTranslationsDto> source)
        {
            return source?.MapList(x => x.ToTasksTranslationEntity());
        }
    }
}
