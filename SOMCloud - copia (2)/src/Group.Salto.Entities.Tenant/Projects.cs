using Group.Salto.Common.Entities;
using System;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class Projects : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public string Serie { get; set; }
        public int Counter { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CollectionsClosureCodesId { get; set; }
        public int? WorkOrderStatusesId { get; set; }
        public string Status { get; set; }
        public string Observations { get; set; }
        public int? IdIcg { get; set; }
        public int CollectionsTypesWorkOrdersId { get; set; }
        public int? CollectionsExtraFieldId { get; set; }
        public int? ContractId { get; set; }
        public int WorkOrderCategoriesCollectionId { get; set; }
        public int? VisibilityPda { get; set; }
        public string DefaultTechnicalCode { get; set; }
        public string BackOfficeResponsible { get; set; }
        public string TechnicalResponsible { get; set; }
        public bool IsActive { get; set; }
        public int? QueuetId { get; set; }

        public CollectionsClosureCodes CollectionsClosureCodes { get; set; }
        public CollectionsExtraField CollectionsExtraField { get; set; }
        public CollectionsTypesWorkOrders CollectionsTypesWorkOrders { get; set; }
        public Contracts Contract { get; set; }
        public WorkOrderCategoriesCollections WorkOrderCategoriesCollection { get; set; }
        public WorkOrderStatuses WorkOrderStatuses { get; set; }
        public Queues Queues { get; set; }
        public ICollection<BillingLineItems> BillingLineItems { get; set; }
        public ICollection<DerivedServices> DerivedServices { get; set; }
        public ICollection<ExternalServicesConfiguration> ExternalServicesConfiguration { get; set; }
        public ICollection<ExternalServicesConfigurationProjectCategories> ExternalServicesConfigurationProjectCategories { get; set; }
        public ICollection<PreconditionsLiteralValues> PreconditionsLiteralValues { get; set; }
        public ICollection<PredefinedServices> PredefinedServices { get; set; }
        public ICollection<ProjectsCalendars> ProjectsCalendars { get; set; }
        public ICollection<ProjectsContacts> ProjectsContacts { get; set; }
        public ICollection<ProjectsPermissions> ProjectPermission { get; set; }
        public ICollection<TechnicalCodes> TechnicalCodes { get; set; }
        public ICollection<WorkOrders> WorkOrders { get; set; }
        public ICollection<WorkOrdersDeritative> WorkOrdersDeritative { get; set; }
        public ICollection<ZoneProject> ZoneProject { get; set; }
        public ICollection<PeopleProjects> PeopleProjects { get; set; }
        public ICollection<FlowsProjects> FlowsProjects {get; set;}
    }
}