using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Language;
using Group.Salto.ServiceLibrary.Common.Dtos.Translation;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderStatus;
using Group.Salto.SOM.Web.Models.MultiCombo;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.WorkOrderStatus;
using NUglify.Helpers;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class WorkOrderStatusViewModelExtensions
    {
        public static ResultViewModel<WorkOrderStatusViewModel> ToResultViewModel(
            this ResultDto<WorkOrderStatusListDto> source, IList<LanguageDto> availableLanguages)
        {
            var response = source != null ? new ResultViewModel<WorkOrderStatusViewModel>()
            {
                Data = source.Data.ToDetailViewModel(availableLanguages),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static WorkOrderStatusViewModel ToDetailViewModel(this WorkOrderStatusListDto source, IList<LanguageDto> availableLanguages)
        {
            WorkOrderStatusViewModel result = null;
            if (source != null)
            {
                result = new WorkOrderStatusViewModel()
                {
                    Name = source.Name,
                    Id = source.Id,
                    Description = source.Description,
                    Color = source.Color,
                    IsWorkOrderClosed = source.IsWorkOrderClosed,
                    IsPlannable = source.IsPlannable,
                };
                result = SeTranslations(result, source.Translations, availableLanguages);
            }

            return result;
        }

        public static WorkOrderStatusListDto ToDto(this WorkOrderStatusViewModel source)
        {
            WorkOrderStatusListDto result = null;
            if (source != null)
            {
                result = new WorkOrderStatusListDto()
                {
                    Name = source.Name,
                    Id = source.Id,
                    Description = source.Description,
                    Color = source.Color,
                    IsWorkOrderClosed = source.IsWorkOrderClosed,
                    IsPlannable = source.IsPlannable,
                };
                result = SetDtoTranslations(result, source);
            }

            return result;
        }

        private static WorkOrderStatusListDto SetDtoTranslations(WorkOrderStatusListDto target,
                                                                    WorkOrderStatusViewModel source)
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

        private static WorkOrderStatusViewModel SeTranslations(WorkOrderStatusViewModel target,
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