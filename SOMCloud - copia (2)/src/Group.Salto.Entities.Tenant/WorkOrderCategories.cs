using Group.Salto.Common.Entities;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class WorkOrderCategories : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public string Info { get; set; }
        public int SlaId { get; set; }
        public int WorkOrderCategoriesCollectionId { get; set; }
        public double? EstimatedDuration { get; set; }
        public double? EconomicCharge { get; set; }
        public int ProjectCalendarPreference { get; set; }
        public int CategoryCalendarPreference { get; set; }
        public int ClientSiteCalendarPreference { get; set; }
        public int SiteCalendarPreference { get; set; }
        public string Serie { get; set; }
        public string Url { get; set; }
        public string DefaultTechnicalCode { get; set; }
        public string TechnicalResponsible { get; set; }
        public string BackOfficeResponsible { get; set; }
        public bool? IsGhost { get; set; }

        public Sla Sla { get; set; }
        public WorkOrderCategoriesCollections WorkOrderCategoriesCollection { get; set; }
        public ICollection<BillingLineItems> BillingLineItems { get; set; }
        public ICollection<ExternalServicesConfiguration> ExternalServicesConfiguration { get; set; }
        public ICollection<ExternalServicesConfigurationProjectCategories> ExternalServicesConfigurationProjectCategories { get; set; }
        public ICollection<PreconditionsLiteralValues> PreconditionsLiteralValues { get; set; }
        public ICollection<TechnicalCodes> TechnicalCodes { get; set; }
        public ICollection<WorkOrderCategoryCalendar> WorkOrderCategoryCalendar { get; set; }
        public ICollection<WorkOrderCategoryKnowledge> WorkOrderCategoryKnowledge { get; set; }
        public ICollection<WorkOrderCategoryPermissions> WorkOrderCategoryPermission { get; set; }
        public ICollection<WorkOrders> WorkOrders { get; set; }
        public ICollection<WorkOrdersDeritative> WorkOrdersDeritative { get; set; }
        public ICollection<WorkOrderCategoryRoles> WorkOrderCategoryRoles { get; set; }
        public ICollection<FlowsWOCategories> FlowsWOCategories { get; set; }
    }
}
