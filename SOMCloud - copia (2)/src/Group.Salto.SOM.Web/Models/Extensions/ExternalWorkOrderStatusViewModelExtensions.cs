using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.ExternalWorkOrderStatus;
using Group.Salto.ServiceLibrary.Common.Dtos.Language;
using Group.Salto.ServiceLibrary.Common.Dtos.Translation;
using Group.Salto.SOM.Web.Models.ExternalWorkOrderStatus;
using Group.Salto.SOM.Web.Models.MultiCombo;
using Group.Salto.SOM.Web.Models.Result;
using NUglify.Helpers;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ExternalWorkOrderStatusViewModelExtensions
    {
        public static ResultViewModel<ExternalWorkOrderStatusViewModel> ToResultViewModel(
            this ResultDto<ExternalWorkOrderStatusListDto> source, IList<LanguageDto> availableLanguages)
        {
            var response = source != null ? new ResultViewModel<ExternalWorkOrderStatusViewModel>()
            {
                Data = source.Data.ToDetailViewModel(availableLanguages),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static ExternalWorkOrderStatusViewModel ToDetailViewModel(this ExternalWorkOrderStatusListDto source, IList<LanguageDto> availableLanguages)
        {
            ExternalWorkOrderStatusViewModel result = null;
            if (source != null)
            {
                result = new ExternalWorkOrderStatusViewModel()
                {
                    Name = source.Name,
                    Id = source.Id,
                    Description = source.Description,
                    Color = source.Color,
                };
                result = SeTranslations(result, source.Translations, availableLanguages);
            }

            return result;
        }

        public static ExternalWorkOrderStatusListDto ToDto(this ExternalWorkOrderStatusViewModel source)
        {
            ExternalWorkOrderStatusListDto result = null;
            if (source != null)
            {
                result = new ExternalWorkOrderStatusListDto()
                {
                    Name = source.Name,
                    Id = source.Id,
                    Description = source.Description,
                    Color = source.Color,
                };
                result = SetDtoTranslations(result, source);
            }

            return result;
        }

        private static ExternalWorkOrderStatusListDto SetDtoTranslations(ExternalWorkOrderStatusListDto target,
            ExternalWorkOrderStatusViewModel source)
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

        private static ExternalWorkOrderStatusViewModel SeTranslations(ExternalWorkOrderStatusViewModel target,
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