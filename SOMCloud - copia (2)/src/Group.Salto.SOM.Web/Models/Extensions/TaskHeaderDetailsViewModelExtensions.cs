using Group.Salto.ServiceLibrary.Common.Dtos.Language;
using Group.Salto.ServiceLibrary.Common.Dtos.Permisions;
using Group.Salto.ServiceLibrary.Common.Dtos.Tasks;
using Group.Salto.ServiceLibrary.Common.Dtos.Translation;
using Group.Salto.SOM.Web.Models.MultiCombo;
using Group.Salto.SOM.Web.Models.Task;
using NUglify.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class TaskHeaderDetailViewModelExtensions
    {
        public static TaskHeaderDetailViewModel ToDetailViewModel(this TasksDetailDto source,
                                                                    IList<LanguageDto> availableLanguages,
                                                                    string title = "")
        {
            TaskHeaderDetailViewModel result = null;
            if (source != null)
            {
                result = new TaskHeaderDetailViewModel
                {
                    Id = source.TaskId,
                    Name = source.Name,
                    Description = source.Description,
                    PermissionsTasksSelected = source.PermissionsTasksSelected,
                    TasksTranslationsList = source.TasksPlainTranslations.ToViewModel(),
                    AvailableLanguages = availableLanguages.ToViewModel()
                };
                result = SetTranslations(result, source.Translations, availableLanguages);
            }
            return result;
        }

        public static TasksDto ToDto(this TaskHeaderViewModel source)
        {
            TasksDto model = new TasksDto
            {
                TaskId = source.Id,
                Name = source.Name,
                Description = source.Description,
                PermissionsTasksSelected = source.PermissionsTasksSelected,
                TasksPlainTranslations = source.TasksTranslationsList.ToDto(),
            };
            return model;
        }

        private static TasksDetailDto SetDtoTranslations(TasksDetailDto target, TaskHeaderDetailViewModel source)
        {
            var listTranslations = new List<ContentTranslationDto>();
            var languageTranslates = source.TextTranslations?.Select(x => x.Value)?.ToList() ?? new List<int>();
            languageTranslates.AddRange(source.DescriptionTranslations?.Select(x => x.Value) ?? new List<int>());
            languageTranslates = languageTranslates.DistinctBy(x => x).ToList();
            foreach (var languagesId in languageTranslates)
            {
                listTranslations.Add(new ContentTranslationDto()
                {
                    Id = Guid.Empty,
                    Description = source.DescriptionTranslations?.FirstOrDefault(x => x.Value == languagesId)?.TextSecondary,
                    Text = source.TextTranslations?.FirstOrDefault(x => x.Value == languagesId)?.TextSecondary,
                    LanguageId = languagesId,
                });
            }

            target.Translations = listTranslations;
            return target;
        }

        private static TaskHeaderDetailViewModel SetTranslations(TaskHeaderDetailViewModel target,
                                                    IList<ContentTranslationDto> translations,
                                                    IList<LanguageDto> availableLanguages)
        {
            IList<MultiComboViewModel<int, int>> textTranslations = new List<MultiComboViewModel<int, int>>();
            IList<MultiComboViewModel<int, int>> descriptionTranslations = new List<MultiComboViewModel<int, int>>();
            if (translations != null && translations.Any() && availableLanguages != null)
            {
                foreach (var translation in translations)
                {
                    var language = availableLanguages.SingleOrDefault(x => x.Id == translation.LanguageId);
                    if (language != null)
                    {
                        if (!string.IsNullOrEmpty(translation.Description))
                        {
                            descriptionTranslations.Add(new MultiComboViewModel<int, int>()
                            {
                                Value = language.Id,
                                Text = language.Name,
                                TextSecondary = translation.Description,
                                CanDelete = true,
                            });
                        }
                        if (!string.IsNullOrEmpty(translation.Text))
                        {
                            textTranslations.Add(new MultiComboViewModel<int, int>()
                            {
                                Value = language.Id,
                                Text = language.Name,
                                TextSecondary = translation.Text,
                                CanDelete = true,
                            });
                        }
                    }
                }
            }

            target.DescriptionTranslations = descriptionTranslations;
            target.TextTranslations = textTranslations;
            return target;
        }
    }
}