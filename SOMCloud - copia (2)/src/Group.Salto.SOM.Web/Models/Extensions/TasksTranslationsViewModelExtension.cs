using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.TasksTranslations;
using Group.Salto.SOM.Web.Models.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class TasksTranslationsViewModelExtension
    {
        public static TasksTranslationsViewModel ToViewModel(this TasksTranslationsDto source)
        {
            TasksTranslationsViewModel result = null;
            if (source != null)
            {
                result = new TasksTranslationsViewModel()
                {
                    Id = source.Id,
                    NameText = source.NameText,
                    DescriptionText = source.DescriptionText,
                    LanguageId = source.LanguageId
                };
            }
            return result;
        }

        public static IList<TasksTranslationsViewModel> ToViewModel(this IList<TasksTranslationsDto> source)
        {
            return source?.MapList(d => d.ToViewModel());
        }

        public static TasksTranslationsDto ToDto(this TasksTranslationsViewModel source)
        {
            TasksTranslationsDto result = null;
            if (source != null)
            {
                result = new TasksTranslationsDto()
                {
                    Id = source.Id,
                    NameText = source.NameText,
                    DescriptionText = source.DescriptionText,
                    LanguageId = source.LanguageId
                };
            }
            return result;
        }

        public static IList<TasksTranslationsDto> ToDto(this IList<TasksTranslationsViewModel> source)
        {
            return source?.MapList(d => d.ToDto());
        }
    }
}
