using System;

namespace Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders
{
    public class WorkOrderEditDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int WorkOrderCategoryId { get; set; }
        public int WorkOrderTypesId { get; set; }
        public string TextRepair { get; set; }
        public string Observations { get; set; }
        public int OriginId { get; set; }
        public int QueueId { get; set; }
        public int WorkOrderStatusId { get; set; }
        public int ExternalWorOrderStatusId { get; set; }
        public int TechnicianId { get; set; }
        public string TechnicianName { get; set; }
        public bool IsResponsibleFixed { get; set; }
        public int ClientSiteId { get; set; }
        public string ClientSiteName { get; set; }
        public int SiteId { get; set; }
        public string SiteName { get; set; }
        public int AssetId { get; set; }
        public string AssetName { get; set; }
        public int UserSiteId { get; set; }
        public string UserSiteName { get; set; }
        public string InternalIdentifier { get; set; }
        public string ClientSiteWorkOrder { get; set; }
        public string CreationDate { get; set; }
        public string PickUpTime { get; set; }
        public string AssignmentTime { get; set; }
        public string ActuationDate { get; set; }
        public string ActuationEndDate { get; set; }
        public string FinalClientClosingTime { get; set; }
        public string InternalClosingTime { get; set; }
        public bool IsActuationDateFixed { get; set; }
        public string ResponseDateSla { get; set; }
        public string ResolutionDateSla { get; set; }
        public string DateUnansweredPenaltySla { get; set; }
        public string DatePenaltyWithoutResolutionSla { get; set; }
        public int PeopleIntroducedById { get; set; }
        public int UserConfigurationId { get; set; }
        public Guid CustomerId { get; set; }
        public int GeneratorServiceId { get; set; }
        public bool? RefGeneratorService { get; set; }
        public bool? RefOtherServices { get; set; }
    }
}