using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Language;
using Group.Salto.ServiceLibrary.Common.Dtos.Permisions;
using Group.Salto.ServiceLibrary.Common.Dtos.Translation;
using Group.Salto.ServiceLibrary.Common.Dtos.Queue;
using Group.Salto.SOM.Web.Models.MultiCombo;
using Group.Salto.SOM.Web.Models.MultiSelect;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.Queue;
using NUglify.Helpers;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class QueueViewModelExtensions
    {
        public static ResultViewModel<QueueViewModel> ToResultViewModel(
            this ResultDto<QueueListDto> source, IList<LanguageDto> availableLanguages, 
                    IList<PermissionsDto> permissions, string title = "")
        {
            var response = source != null ? new ResultViewModel<QueueViewModel>()
            {
                Data = source.Data.ToDetailViewModel(availableLanguages, permissions, title),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static QueueViewModel ToDetailViewModel(this QueueListDto source, 
                                                        IList<LanguageDto> availableLanguages, 
                                                        IList<PermissionsDto> permissions, string title = "")
        {
            QueueViewModel result = null;
            if (source != null)
            {
                result = new QueueViewModel()
                {
                    Name = source.Name,
                    Id = source.Id,
                    Description = source.Description,
                };
                result = SeTranslations(result, source.Translations, availableLanguages);
                result = SetPermissions(result, source.PermissionsSelected, permissions, title);
            }

            return result;
        }



        public static QueueListDto ToDto(this QueueViewModel source)
        {
            QueueListDto result = null;
            if (source != null)
            {
                result = new QueueListDto()
                {
                    Name = source.Name,
                    Id = source.Id,
                    Description = source.Description,
                    PermissionsSelected = source.Permissions?
                                                .Items?
                                                .Where(x=>x.IsChecked && int.TryParse(x.Value, out var temp))?
                                                .Select(x=>int.Parse(x.Value))?.ToList()
                };
                result = SetDtoTranslations(result, source);
            }

            return result;
        }

        private static QueueListDto SetDtoTranslations(QueueListDto target,
                                                        QueueViewModel source)
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

        private static QueueViewModel SetPermissions(QueueViewModel target,
                                                    IList<int> permissionsSelected,
                                                    IList<PermissionsDto> permissions, string title = "")
        {
            if (permissions != null && permissions.Any())
            {
                target.Permissions = new MultiSelectViewModel(title);
                target.Permissions.Items = new List<MultiSelectItem>();
                foreach (var permission in permissions)
                {
                    target.Permissions.Items.Add(new MultiSelectItem()
                    {
                        Value = permission.Id.ToString(),
                        IsChecked = permissionsSelected?.Any(x=>x == permission.Id)?? false,
                        LabelName = permission.Name
                    });
                }
            }

            return target;
        }

        private static QueueViewModel SeTranslations(QueueViewModel target,
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