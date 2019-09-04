using Group.Salto.Common.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Group.Salto.Entities.Tenant
{
    public class People : SoftDeleteBaseEntity
    {
        public string Dni { get; set; }
        public string Name { get; set; }
        public string FisrtSurname { get; set; }
        public string SecondSurname { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public int IsClientWorker { get; set; }
        public int Priority { get; set; }
        public int? IcgId { get; set; }
        public int? SubcontractId { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? WorkRadiusKm { get; set; }
        public bool? IsActive { get; set; }
        public bool? SubcontractorResponsible { get; set; }
        public string WarehouseId { get; set; }
        public string Extension { get; set; }
        public int? CompanyId { get; set; }
        public double? CostKm { get; set; }
        public int? DepartmentId { get; set; }
        public string DocumentationUrl { get; set; }
        public int? PointsRateId { get; set; }
        public int? UserConfigurationId { get; set; }
        public bool IsVisible { get; set; }
        public int? AnnualHours { get; set; }
        public int? ResponsibleId { get; set; }
        public int? WorkCenterId { get; set; }
        public int? WarehousesId { get; set; }
        [NotMapped]
        public string FullName { get { return $"{Name} {FisrtSurname} {SecondSurname}"; } }
        [NotMapped]
        public string NameSurname { get { return $"{Name} {FisrtSurname}"; } }

        public Warehouses Warehouses { get; set; }
        public Companies Company { get; set; }
        public Departments Department { get; set; }
        public PointsRate PointsRate { get; set; }
        public SubContracts Subcontract { get; set; }
        public UserConfiguration UserConfiguration { get; set; }
        public WorkCenters WorkCenter { get; set; }
        public ICollection<WorkCenters> WorkCenters { get; set; }
        public ICollection<AdvancedTechnicianListFilterPersons> AdvancedTechnicianListFilterPersons { get; set; }
        public ICollection<Bill> Bill { get; set; }
        public ICollection<CalendarPlanningViewConfigurationPeople> CalendarPlanningViewConfigurationPeople { get; set; }
        public ICollection<Contracts> Contracts { get; set; }
        public ICollection<DerivedServices> DerivedServices { get; set; }
        public ICollection<ExpensesTickets> ExpensesTicketsPeople { get; set; }
        public ICollection<ExpensesTickets> ExpensesTicketsPeopleValidator { get; set; }
        public ICollection<FinalClients> FinalClients { get; set; }
        public ICollection<Journeys> Journeys { get; set; }
        public ICollection<KnowledgePeople> KnowledgePeople { get; set; }
        public ICollection<Locations> Locations { get; set; }
        public ICollection<LocationsFinalClients> LocationsFinalClients { get; set; }
        public ICollection<MainWoViewConfigurationsPeople> MainWoViewConfigurationsPeople { get; set; }
        public ICollection<PeopleCalendars> PeopleCalendars { get; set; }
        public ICollection<PeopleCollectionsAdmins> PeopleCollectionsAdmins { get; set; }
        public ICollection<PeopleCollectionsPeople> PeopleCollectionsPeople { get; set; }
        public ICollection<PeopleCost> PeopleCost { get; set; }
        public ICollection<PeopleCostHistorical> PeopleCostHistorical { get; set; }
        public ICollection<PeopleRegisteredPda> PeopleRegisteredPda { get; set; }
        public ICollection<PlanificationCriterias> PlanificationCriterias { get; set; }
        public ICollection<PlanningPanelViewConfiguration> PlanningPanelViewConfiguration { get; set; }
        public ICollection<PlanningPanelViewConfigurationPeople> PlanningPanelViewConfigurationPeople { get; set; }
        public ICollection<Postconditions> PostconditionsPeopleManipulator { get; set; }
        public ICollection<Postconditions> PostconditionsPeopleTechnicians { get; set; }
        public ICollection<PreconditionsLiteralValues> PreconditionsLiteralValuesPeopleManipulator { get; set; }
        public ICollection<PreconditionsLiteralValues> PreconditionsLiteralValuesPeopleTechnician { get; set; }
        public ICollection<Services> Services { get; set; }
        public ICollection<Tasks> TasksPeopleManipulator { get; set; }
        public ICollection<Tasks> TasksPeopleTechnician { get; set; }
        public ICollection<TechnicalCodes> TechnicalCodes { get; set; }
        public ICollection<TechnicianListFilters> TechnicianListFilters { get; set; }
        public ICollection<Vehicles> Vehicles { get; set; }
        public ICollection<WorkOrdersDeritative> WorkOrdersDeritativePeopleIntroducedBy { get; set; }
        public ICollection<WorkOrdersDeritative> WorkOrdersDeritativePeopleManipulator { get; set; }
        public ICollection<WorkOrdersDeritative> WorkOrdersDeritativePeopleResponsible { get; set; }
        public ICollection<WorkOrders> WorkOrdersPeopleIntroducedBy { get; set; }
        public ICollection<WorkOrders> WorkOrdersPeopleManipulator { get; set; }
        public ICollection<WorkOrders> WorkOrdersPeopleResponsible { get; set; }
        public ICollection<PeoplePermissions> PeoplePermissions { get; set; }
        public ICollection<PeopleProjects> PeopleProjects { get; set; }
        public ICollection<PushNotificationsPeople> PushNotificationsPeople { get; set; }
        public ICollection<DnAndMaterialsAnalysis> DnAndMaterialsAnalysis { get; set; }
        public ICollection<PeopleNotification> PeopleNotifications { get; set; }
        public ICollection<PeoplePushRegistration> PeoplePushRegistrations { get; set; }
    }
}
