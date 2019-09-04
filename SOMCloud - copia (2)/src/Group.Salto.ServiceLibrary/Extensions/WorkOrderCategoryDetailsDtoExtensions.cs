using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderCategories;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class WorkOrderCategoryDetailsDtoExtensions
    {
        public static WorkOrderCategoryDetailsDto ToDetailDto(this WorkOrderCategories source)
        {
            WorkOrderCategoryDetailsDto result = null;
            if (source != null)
            {
                result = new WorkOrderCategoryDetailsDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    IdSLA = source.SlaId,
                    Url = source.Url,
                    EstimatedDuration = source.EstimatedDuration.HasValue ? source.EstimatedDuration.Value : 0,
                    EconomicCharge = source.EconomicCharge.HasValue ? source.EconomicCharge.Value : 0,
                    Serie = source.Serie,
                    IsGhost = source.IsGhost.HasValue ? source.IsGhost.Value : false,
                    WorkOrderCategoriesCollectionId = source.WorkOrderCategoriesCollectionId,
                    DefaultTechnicalCode = source.DefaultTechnicalCode,
                    TechnicalCodesSelected = source.TechnicalCodes?.ToList()?.ToTechnicalCodesDto(),
                    ClientSiteCalendarPreference = source.ClientSiteCalendarPreference,
                    ProjectCalendarPreference = source.ProjectCalendarPreference,
                    CategoryCalendarPreference = source.CategoryCalendarPreference,
                    SiteCalendarPreference = source.SiteCalendarPreference,
                    KnowledgeSelected = source.WorkOrderCategoryKnowledge?.ToList()?.ToWorkOrderCategoryKnowledgeDto(),
                    BackOfficeResponsible = source.BackOfficeResponsible,
                    TechnicalResponsible = source.TechnicalResponsible,
                };
            }
            return result;
        }

        public static WorkOrderCategories ToEntity(this WorkOrderCategoryDetailsDto source)
        {
            WorkOrderCategories result = null;
            if (source != null)
            {
                result = new WorkOrderCategories()
                {
                    Name = source.Name,
                    SlaId = source.IdSLA,
                    Url = source.Url,
                    EstimatedDuration = source.EstimatedDuration,
                    EconomicCharge = source.EconomicCharge,
                    Serie = source.Serie,
                    IsGhost = source.IsGhost,
                    DefaultTechnicalCode = source.DefaultTechnicalCode,
                    WorkOrderCategoriesCollectionId = source.WorkOrderCategoriesCollectionId,
                    ClientSiteCalendarPreference = source.ClientSiteCalendarPreference,
                    ProjectCalendarPreference = source.ProjectCalendarPreference,
                    CategoryCalendarPreference = source.CategoryCalendarPreference,
                    SiteCalendarPreference = source.SiteCalendarPreference,
                    BackOfficeResponsible = source.BackOfficeResponsible,
                    TechnicalResponsible = source.TechnicalResponsible,
                };
            }
            return result;
        }

        public static void UpdateWorkOrderCategory(this WorkOrderCategories target, WorkOrderCategories source)
        {
            if (source != null && target != null)
            {
                target.Name = source.Name;
                target.SlaId = source.SlaId;
                target.Url = source.Url;
                target.EstimatedDuration = source.EstimatedDuration;
                target.EconomicCharge = source.EconomicCharge;
                target.Serie = source.Serie;
                target.IsGhost = source.IsGhost;
                target.DefaultTechnicalCode = source.DefaultTechnicalCode;
                target.WorkOrderCategoriesCollectionId = source.WorkOrderCategoriesCollectionId;
                target.ClientSiteCalendarPreference = source.ClientSiteCalendarPreference;
                target.ProjectCalendarPreference = source.ProjectCalendarPreference;
                target.CategoryCalendarPreference = source.CategoryCalendarPreference;
                target.SiteCalendarPreference = source.SiteCalendarPreference;
                target.BackOfficeResponsible = source.BackOfficeResponsible;
                target.TechnicalResponsible = source.TechnicalResponsible;
            }
        }

        public static bool IsValid(this WorkOrderCategoryDetailsDto source)
        {
            bool result = false;
            result = source != null;
            return result;
        }
    }
}