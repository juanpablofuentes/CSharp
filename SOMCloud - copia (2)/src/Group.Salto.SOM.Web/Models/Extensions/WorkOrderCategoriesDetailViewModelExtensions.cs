using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.TechnicalCodes;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderCategories;
using Group.Salto.SOM.Web.Models.Knowledge;
using Group.Salto.SOM.Web.Models.MultiCombo;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.Technicians;
using Group.Salto.SOM.Web.Models.WorkOrderCategory;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class WorkOrderCategoriesDetailViewModelExtensions
    {
        public static WorkOrderCategoryDetailViewModel ToViewModel(this WorkOrderCategoryDetailsDto source)
        {
            WorkOrderCategoryDetailViewModel result = null;
            if (source != null)
            {
                result = new WorkOrderCategoryDetailViewModel();
                result.GenericEditViewModel = new  GenericEditViewModel()
                {
                    WorkOrderCategoriesCollectionId = source.WorkOrderCategoriesCollectionId,
                    Id = source.Id,
                    Name = source.Name,
                    IdSLA = source.IdSLA,
                    Url = source.Url,
                    Duration = ((decimal)source.EstimatedDuration).DecimalToString(CultureInfo.InvariantCulture.TwoLetterISOLanguageName),
                    EconomicCharge = ((decimal)source.EconomicCharge).DecimalToString(CultureInfo.InvariantCulture.TwoLetterISOLanguageName),
                    Serie = source.Serie,
                    IsGhost = source.IsGhost,
                    DefaultTechnicalCode = source.DefaultTechnicalCode,
                    CategoryCalendarPreference = source.CategoryCalendarPreference,
                    ClientSiteCalendarPreference = source.ClientSiteCalendarPreference,
                    ProjectCalendarPreference = source.ProjectCalendarPreference,
                    SiteCalendarPreference = source.SiteCalendarPreference,
                    BackOfficeResponsible = source.BackOfficeResponsible,
                    TechnicalResponsible = source.TechnicalResponsible,
                };
                result.TechniciansSelected = source.TechnicalCodesSelected.ToTechniciansEditViewModel();
                result.KnowledgeSelected = source.KnowledgeSelected.ToMultiComboViewModel();
            }
            return result;
        }

        public static ResultViewModel<WorkOrderCategoryDetailViewModel> ToViewModel(this ResultDto<WorkOrderCategoryDetailsDto> source)
        {
            var response = source != null ? new ResultViewModel<WorkOrderCategoryDetailViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static WorkOrderCategoryDetailsDto ToDto(this WorkOrderCategoryDetailViewModel genericVM)
        {
            WorkOrderCategoryDetailsDto result = null;
            if (genericVM != null)
            {
                result = new WorkOrderCategoryDetailsDto()
                {
                    Id = genericVM.GenericEditViewModel.Id,
                    WorkOrderCategoriesCollectionId = genericVM.GenericEditViewModel.WorkOrderCategoriesCollectionId,
                    Name = genericVM.GenericEditViewModel.Name,
                    IdSLA = genericVM.GenericEditViewModel.IdSLA,
                    Url = genericVM.GenericEditViewModel.Url,
                    EstimatedDuration = (double)genericVM.GenericEditViewModel.Duration?.StringToDecimal(CultureInfo.InvariantCulture.TwoLetterISOLanguageName),
                    EconomicCharge = (double)genericVM.GenericEditViewModel.EconomicCharge?.StringToDecimal(CultureInfo.InvariantCulture.TwoLetterISOLanguageName),
                    Serie = genericVM.GenericEditViewModel.Serie,
                    IsGhost = genericVM.GenericEditViewModel.IsGhost,
                    DefaultTechnicalCode = genericVM.GenericEditViewModel.DefaultTechnicalCode,
                    TechnicalCodesSelected = genericVM.TechniciansSelected.ToTechnicalCodesDto(),
                    Permissions = genericVM.Permissions.Items.ToDto(),
                    Roles = genericVM.Roles.Items.ToDto(),
                    CategoryCalendarPreference = genericVM.GenericEditViewModel.CategoryCalendarPreference,
                    ClientSiteCalendarPreference = genericVM.GenericEditViewModel.ClientSiteCalendarPreference,
                    ProjectCalendarPreference = genericVM.GenericEditViewModel.ProjectCalendarPreference,
                    SiteCalendarPreference = genericVM.GenericEditViewModel.SiteCalendarPreference,
                    KnowledgeSelected = genericVM.KnowledgeSelected.ToWorkOrderCategoryKnowledgeDto(),
                    BackOfficeResponsible = genericVM.GenericEditViewModel.BackOfficeResponsible,
                    TechnicalResponsible = genericVM.GenericEditViewModel.TechnicalResponsible,
                };
            }
            return result;
        }

        public static TechniciansEditViewModel ToTechniciansEditViewModel(this TechnicalCodesDto source)
        {
            TechniciansEditViewModel result = null;
            if (source != null)
            {
                result = new TechniciansEditViewModel()
                {
                    TechniciansId = source.Id,
                    TechniciansName = source.Name,
                    PeopleId = source.PeopleId,
                    Code = source.Code,
                };
            }
            return result;
        }

        public static IList<TechniciansEditViewModel> ToTechniciansEditViewModel(this IList<TechnicalCodesDto> source)
        {
            return source?.MapList(pk => pk.ToTechniciansEditViewModel());
        }

        public static TechnicalCodesDto ToTechnicalCodesDto(this TechniciansEditViewModel source)
        {
            TechnicalCodesDto result = null;
            if (source != null)
            {
                result = new TechnicalCodesDto()
                {
                    Id = source.TechniciansId,
                    PeopleId = source.PeopleId,
                    Code = source.Code,
                    Name = source.TechniciansName
                };
            }
            return result;
        }

        public static IList<TechnicalCodesDto> ToTechnicalCodesDto(this IList<TechniciansEditViewModel> source)
        {
            return source?.MapList(pk => pk.ToTechnicalCodesDto());
        }

        public static MultiComboViewModel<int, int> ToMultiComboViewModel(this WorkOrderCategoryKnowledgeDto source)
        {
            MultiComboViewModel<int, int> result = null;
            if (source != null)
            {
                result = new MultiComboViewModel<int, int>()
                {
                    Value = source.Id,
                    ValueSecondary = source.Priority,
                    TextSecondary = source.Priority.ToString("00"),
                    Text = source.Name,
                };
            }
            return result;
        }

        public static IList<MultiComboViewModel<int, int>> ToMultiComboViewModel(this IList<WorkOrderCategoryKnowledgeDto> source)
        {
            return source?.MapList(pk => pk.ToMultiComboViewModel());
        }

        public static WorkOrderCategoryKnowledgeDto ToWorkOrderCategoryKnowledgeDto(this MultiComboViewModel<int, int> source)
        {
            WorkOrderCategoryKnowledgeDto result = null;
            if (source != null)
            {
                result = new WorkOrderCategoryKnowledgeDto()
                {
                    Id = source.Value,
                    Name = source.Text,
                    Priority = source.ValueSecondary
                };
            }
            return result;
        }

        public static IList<WorkOrderCategoryKnowledgeDto> ToWorkOrderCategoryKnowledgeDto(this IList<MultiComboViewModel<int, int>> source)
        {
            return source?.MapList(pk => pk.ToWorkOrderCategoryKnowledgeDto());
        }
    }
}