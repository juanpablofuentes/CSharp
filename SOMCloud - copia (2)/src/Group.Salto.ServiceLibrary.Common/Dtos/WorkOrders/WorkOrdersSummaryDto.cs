using System;

namespace Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders
{
    public class WorkOrdersSummaryDto
    {
        public string ProjectName { get; set; }
        public string ProjectObservations { get; set; }
        public string ContractName { get; set; }
        public string ContractObservations { get; set; }
        public string FinalClientName { get; set; }
        public string FinalClientNIF { get; set; }
        public string FinalClientTel1 { get; set; }
        public string FinalClientTel2 { get; set; }
        public string FinalClientTel3 { get; set; }
        public string FinalClientFax { get; set; }
        public string FinalClientObservations { get; set; }
        public string LocationName { get; set; }
        public string LocationZone { get; set; }
        public string LocationSubZone { get; set; }
        public string LocationCity { get; set; }
        public int? LocationPostalCode { get; set; }
        public string LocationProvince { get; set; }
        public string AssignationQueue { get; set; }
        public string AssignationTechnician { get; set; }
        public int? WorkOrderFatherID { get; set; }
        public string WorkOrderType { get; set; }
        public string WorkOrderCategory { get; set; }
        public string WorkOrderClient { get; set; }
        public string WorkOrderClientSite { get; set; }
        public string WorkOrderStatus { get; set; }
        public string WorkOrderExternalStatus { get; set; }
        public string WorkOrderRepair { get; set; }
        public string TimingCreation { get; set; }
        public string TimingReception { get; set; }
        public string TimingAssigned { get; set; }
        public string TimingPerformance { get; set; }
        public string TimingFinalPerformance { get; set; }
        public string TimingCloseClient { get; set; }
        public string TimingCloseClientSalto { get; set; }
        public string TimingSLAResponse { get; set; }
        public string TimingFinalSLA { get; set; }
        public string TimingResponsePenalty { get; set; }
        public string TimingBreachPenalty { get; set; }
        public string TimingStopSLA { get; set; }
    }
}