using Group.Salto.ServiceLibrary.Common.Dtos.TechnicalCodes;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderCategories
{
    public class WorkOrderCategoryDetailsDto: WorkOrderCategoriesListDto
    {
        public int WorkOrderCategoriesCollectionId { get; set; }
        public int IdSLA { get; set; }
        public string Url { get; set; }
        public double EconomicCharge { get; set; }
        public string Serie { get; set; }
        public bool IsGhost { get; set; }
        public string DefaultTechnicalCode { get; set; }
        public IList<TechnicalCodesDto> TechnicalCodesSelected { get; set; }
        public IList<MultiSelectItemDto> Permissions { get; set; }
        public IList<MultiSelectItemDto> Roles { get; set; }
        public IList<WorkOrderCategoryKnowledgeDto> KnowledgeSelected { get; set; }
        public int ClientSiteCalendarPreference { get; set; }
        public int ProjectCalendarPreference { get; set; }
        public int CategoryCalendarPreference { get; set; }
        public int SiteCalendarPreference { get; set; }
        public string TechnicalResponsible { get; set; }
        public string BackOfficeResponsible { get; set; }
    }
}