using Group.Salto.Common;
using System;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class WorkOrders : BaseEntity
    {
        public string InternalIdentifier { get; set; }
        public string ExternalIdentifier { get; set; }
        public DateTime CreationDate { get; set; }
        public string TextRepair { get; set; }
        public string Observations { get; set; }
        public DateTime? PickUpTime { get; set; }
        public DateTime? FinalClientClosingTime { get; set; }
        public DateTime? InternalClosingTime { get; set; }
        public DateTime? AssignmentTime { get; set; }
        public DateTime? ActionDate { get; set; }
        public int? PeopleResponsibleId { get; set; }
        public int OriginId { get; set; }
        public int PeopleIntroducedById { get; set; }
        public int? WorkOrderTypesId { get; set; }
        public int LocationId { get; set; }
        public int FinalClientId { get; set; }
        public int WorkOrderStatusId { get; set; }
        public int? IcgId { get; set; }
        public int? AssetId { get; set; }
        public int? PeopleManipulatorId { get; set; }
        public int QueueId { get; set; }
        public int? ExternalWorOrderStatusId { get; set; }
        public int ProjectId { get; set; }
        public int? WorkOrdersFatherId { get; set; }
        public DateTime? DateStopTimerSla { get; set; }
        public DateTime? ResponseDateSla { get; set; }
        public DateTime? ResolutionDateSla { get; set; }
        public DateTime? DateUnansweredPenaltySla { get; set; }
        public DateTime? DatePenaltyWithoutResolutionSla { get; set; }
        public bool ReferenceGeneratorService { get; set; }
        public bool ReferenceOtherServices { get; set; }
        public int WorkOrderCategoryId { get; set; }
        public bool Archived { get; set; }
        public DateTime? ActuationEndDate { get; set; }
        public DateTime? LastModified { get; set; }
        public bool IsResponsibleFixed { get; set; }
        public bool IsActuationDateFixed { get; set; }
        public int? SiteUserId { get; set; }
        public bool Billable { get; set; }
        public string ExternalSystemId { get; set; }
        public DateTime? ClosingOtdate { get; set; }
        public DateTime? AccountingClosingDate { get; set; }
        public DateTime? ClientClosingDate { get; set; }
        public DateTime? SystemDateWhenOtclosed { get; set; }
        public DateTime? InternalCreationDate { get; set; }
        public bool? MeetSlaresponse { get; set; }
        public bool? MeetSlaresolution { get; set; }
        public int? Overhead { get; set; }
        public int? GeneratorServiceId { get; set; }

        public ExternalWorOrderStatuses ExternalWorOrderStatus { get; set; }
        public FinalClients FinalClient { get; set; }
        public Locations Location { get; set; }
        public People PeopleIntroducedBy { get; set; }
        public People PeopleManipulator { get; set; }
        public People PeopleResponsible { get; set; }
        public Projects Project { get; set; }
        public Queues Queue { get; set; }
        public SiteUser SiteUser { get; set; }
        public Assets Asset { get; set; }
        public WorkOrderCategories WorkOrderCategory { get; set; }
        public WorkOrderStatuses WorkOrderStatus { get; set; }
        public WorkOrderTypes WorkOrderTypes { get; set; }
        public WorkOrders WorkOrdersFather { get; set; }
        public WorkOrderAnalysis WorkOrderAnalysis { get; set; }
        public Services Service { get; set; }
        public ICollection<Audits> Audits { get; set; }
        public ICollection<Bill> Bill { get; set; }
        public ICollection<ExpensesTickets> ExpensesTickets { get; set; }
        public ICollection<ExternalSystemImportData> ExternalSystemImportData { get; set; }
        public ICollection<WorkOrders> InverseWorkOrdersFather { get; set; }
        public ICollection<ServicesAnalysis> ServicesAnalysis { get; set; }
        public ICollection<SgsClosingInfo> SgsClosingInfo { get; set; }
        public ICollection<AssetsWorkOrders> AssetsWorkOrders { get; set; }
        public ICollection<Services> Services { get; set; }
        public ICollection<WarehouseMovements>  WarehouseMovements { get; set; }
        public ICollection<DnAndMaterialsAnalysis> DnAndMaterialsAnalysis { get; set; }
    }
}
