using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.CollectionsExtraFieldExtraField;
using Group.Salto.ServiceLibrary.Common.Dtos.ExtraFields;
using Group.Salto.ServiceLibrary.Common.Dtos.ExtraFieldTypes;
using Group.Salto.ServiceLibrary.Common.Dtos.Language;
using Group.Salto.ServiceLibrary.Common.Dtos.Translation;
using Group.Salto.SOM.Web.Models.ExtraFields;
using Group.Salto.SOM.Web.Models.MultiCombo;
using Group.Salto.SOM.Web.Models.Result;
using NUglify.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ExtraFieldsDetailViewModelExtensions
    {
        public static ExtraFieldsDetailViewModel ToViewModel(this ExtraFieldsDetailDto source)
        {
            ExtraFieldsDetailViewModel result = null;
            if (source != null)
            {
                result = new ExtraFieldsDetailViewModel()
                {
                    ExtraFieldsId = source.Id,
                    ExtraFieldsName = source.Name,
                    ExtraFieldsDescription = source.Description,
                    TypeId = source.TypeId,
                    TypeName = source.TypeName,
                    ErpSystemInstanceQueryId = source.ErpSystemInstanceQueryId.HasValue ? source.ErpSystemInstanceQueryId.Value : 0 ,
                    ErpSystemInstanceQueryName = source.ErpSystemInstanceQueryName,
                    AllowedStringValues = source.AllowedStringValues,
                    MultipleChoice = source.MultipleChoice.HasValue ? source.MultipleChoice.Value : false,
                    IsMandatory = source.IsMandatory.HasValue ? source.IsMandatory.Value : false,
                    DelSystem = source.DelSystem.HasValue ? source.DelSystem.Value : false,
                };
            }
            return result;
        }

        public static ExtraFieldsDetailViewModel ToViewModelWithLanguages(this ExtraFieldsDetailDto source, IList<LanguageDto> availableLanguages)
        {
            ExtraFieldsDetailViewModel result = null;
            if (source != null)
            {
                result = new ExtraFieldsDetailViewModel()
                {
                    ExtraFieldsId = source.Id,
                    ExtraFieldsName = source.Name,
                    ExtraFieldsDescription = source.Description,
                    TypeId = source.TypeId,
                    ErpSystemInstanceQueryId = source.ErpSystemInstanceQueryId.HasValue ? source.ErpSystemInstanceQueryId.Value : 0,
                    ErpSystemInstanceQueryName = source.ErpSystemInstanceQueryName,
                    AllowedStringValues = source.AllowedStringValues,
                    MultipleChoice = source.MultipleChoice.HasValue ? source.MultipleChoice.Value : false,
                    IsMandatory = source.IsMandatory.HasValue ? source.IsMandatory.Value : false,
                    DelSystem = source.DelSystem.HasValue ? source.DelSystem.Value : false,
                };
            }
            result = SetTranslations(result, source.Translations, availableLanguages);
            return result;
        }

        public static ResultViewModel<ExtraFieldsDetailViewModel> ToResultDetailViewModel(this ResultDto<ExtraFieldsDetailDto> source)
        {
            var response = source != null ? new ResultViewModel<ExtraFieldsDetailViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static IList<ExtraFieldsDetailViewModel> ToListViewModel(this IList<ExtraFieldsDetailDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }

        public static ResultViewModel<ExtraFieldsDetailViewModel> ToViewModelWithLanguages(this ResultDto<ExtraFieldsDetailDto> source, IList<LanguageDto> availableLanguages)
        {
            var response = source != null ? new ResultViewModel<ExtraFieldsDetailViewModel>()
            {
                Data = source.Data.ToViewModelWithLanguages(availableLanguages),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static ExtraFieldsDetailViewModel ToDetailViewModel(this ExtraFieldsDetailDto source, IList<LanguageDto> availableLanguages)
        {
            ExtraFieldsDetailViewModel result = null;
            if (source != null)
            {
                result = new ExtraFieldsDetailViewModel()
                {
                    ExtraFieldsId = source.Id,
                    ExtraFieldsName = source.Name,
                    ExtraFieldsDescription = source.Description,
                };
                result = SetTranslations(result, source.Translations, availableLanguages);
            }

            return result;
        }

        public static ExtraFieldsDetailDto ToDto(this ExtraFieldsDetailViewModel source)
        {
            ExtraFieldsDetailDto result = null;
            if (source != null)
            {
                result = new ExtraFieldsDetailDto()
                {
                    Name = source.ExtraFieldsName,
                    Id = source.ExtraFieldsId,
                    Description = source.ExtraFieldsDescription,
                    AllowedStringValues = source.AllowedStringValues,
                    TypeId = source.TypeId,
                    IsMandatory = source.IsMandatory,
                    MultipleChoice = source.MultipleChoice,
                    ErpSystemInstanceQueryId = source.ErpSystemInstanceQueryId,
                    DelSystem = source.DelSystem,
                };
                result = SetDtoTranslations(result, source);
            }

            return result;
        }

        public static ExtraFieldsDetailViewModel ToExtraFieldsDetail(this CollectionsExtraFieldExtraFieldDto source, IList<ExtraFieldsTypesDto> extraFieldsTypes)
        {
            ExtraFieldsDetailViewModel result = null;
            if (source != null)
            {
                ExtraFieldsTypesDto extraFieldType = extraFieldsTypes.Where(x => x.Id == source.ExtraField.TypeId).FirstOrDefault();
                result = new ExtraFieldsDetailViewModel()
                {
                    Position = source.Position ?? 0,
                    ExtraFieldsId = source.ExtraFieldId,
                    ExtraFieldsName = source.ExtraField.Name,
                    ExtraFieldsDescription = source.ExtraField.Description,
                    TypeId = extraFieldType.Id,
                    TypeName = extraFieldType.Name,
                    ErpSystemInstanceQueryId = source.ExtraField.ErpSystemInstanceQueryId.HasValue ? source.ExtraField.ErpSystemInstanceQueryId.Value : 0,
                    ErpSystemInstanceQueryName = source.ExtraField.ErpSystemInstanceQueryName ?? string.Empty,
                    AllowedStringValues = source.ExtraField.AllowedStringValues ?? string.Empty,
                    MultipleChoice = source.ExtraField.MultipleChoice.HasValue ? source.ExtraField.MultipleChoice.Value : false,
                    IsMandatory = source.ExtraField.IsMandatory.HasValue ? source.ExtraField.IsMandatory.Value : false,
                    DelSystem = source.ExtraField.DelSystem.HasValue ? source.ExtraField.DelSystem.Value : false,
                };
            }
            return result;
        }

        public static IList<ExtraFieldsDetailViewModel> ToExtraFieldsDetail(this IList<CollectionsExtraFieldExtraFieldDto> source, IList<ExtraFieldsTypesDto> extraFieldsTypes)
        {
            return source?.MapList(pk => pk.ToExtraFieldsDetail(extraFieldsTypes));
        }

        private static ExtraFieldsDetailDto SetDtoTranslations(ExtraFieldsDetailDto target, ExtraFieldsDetailViewModel source)
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

        private static ExtraFieldsDetailViewModel SetTranslations(ExtraFieldsDetailViewModel target,
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



        public static CollectionsExtraFieldExtraFieldDto ToCollectionsExtraFieldExtraFieldDto(this ExtraFieldsDetailViewModel source)
        {
            CollectionsExtraFieldExtraFieldDto result = null;
            if (source != null)
            {
                result = new CollectionsExtraFieldExtraFieldDto()
                {
                    ExtraFieldId = source.ExtraFieldsId,
                    Position = source.Position,
                };
                result.ExtraField = new ExtraFieldsDto
                {
                    Id = source.ExtraFieldsId,
                    Name = source.ExtraFieldsName,
                    Description = source.ExtraFieldsDescription,
                    TypeId = source.TypeId,
                    ErpSystemInstanceQueryId = (source.ErpSystemInstanceQueryId.HasValue && source.ErpSystemInstanceQueryId.Value != 0) ? source.ErpSystemInstanceQueryId.Value : (int?)null,
                    AllowedStringValues = source.AllowedStringValues ?? string.Empty,
                    MultipleChoice = source.MultipleChoice,
                    IsMandatory = source.IsMandatory,
                    DelSystem = source.DelSystem,
                    State = source.State
                };
            }
            return result;
        }

        public static IList<CollectionsExtraFieldExtraFieldDto> ToCollectionsExtraFieldExtraFieldDto(this IList<ExtraFieldsDetailViewModel> source)
        {
            return source?.MapList(pk => pk.ToCollectionsExtraFieldExtraFieldDto());
        }
    }
}