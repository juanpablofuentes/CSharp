using System;

namespace Group.Salto.Entities.Tenant
{
    public class WorkOrderAnalysis
    {
        public int WorkOrderCode { get; set; }
        public string WorkOrderClientCode { get; set; }
        public string WorkOrderCampainCode { get; set; }
        public int? InternalAssetCode { get; set; }
        public int ProjectCode { get; set; }
        public int AssignedTechnicianCode { get; set; }
        public DateTime? ClientCreationDate { get; set; }
        public DateTime InternalCreationDate { get; set; }
        public DateTime? ActuationDate { get; set; }
        public DateTime? ClosingClientTime { get; set; }
        public DateTime? InternalSystemTimeWhenOtclosed { get; set; }
        public DateTime? SlaresponseDate { get; set; }
        public DateTime? SlaresolutionDate { get; set; }
        public bool? MeetResponseSla { get; set; }
        public bool? MeetResolutionSla { get; set; }
        public int? ResponseTime { get; set; }
        public int? ResolutionTime { get; set; }
        public int TotalWorkedTime { get; set; }
        public int? ExpectedTimeWorked { get; set; }
        public decimal? ExpectedvsWorkedTime { get; set; }
        public int? OnSiteTime { get; set; }
        public int? TravelTime { get; set; }
        public int? WaitTime { get; set; }
        public int? TotalTime { get; set; }
        public decimal? Kilometers { get; set; }
        public decimal? Tolls { get; set; }
        public decimal? Parking { get; set; }
        public decimal? Expenses { get; set; }
        public decimal? OtherCosts { get; set; }
        public int WorkOrderCategory { get; set; }
        public int? NumberOfVisitsToClient { get; set; }
        public int? NumberOfIntervention { get; set; }
        public int Otstatus { get; set; }
        public int? ExternalOtstatus { get; set; }
        public int FinalClientCode { get; set; }
        public string FinalClientName { get; set; }
        public int? LocationCode { get; set; }
        public string LocationName { get; set; }
        public string LocationAddress { get; set; }
        public string LocationCity { get; set; }
        public string LocationState { get; set; }
        public string LocationRegion { get; set; }
        public string LocationPostalCode { get; set; }
        public string LocationCountry { get; set; }
        public string LocationTown { get; set; }
        public string LocationObservation { get; set; }
        public string LocationClientCode { get; set; }
        public DateTime? ClosingClientDate { get; set; }
        public DateTime? ClosingSystemDate { get; set; }
        public DateTime ClosingWodate { get; set; }
        public DateTime? AccountingClosingDate { get; set; }
        public decimal? TotalWosalesAmount { get; set; }
        public decimal? TotalWoproductionCost { get; set; }
        public decimal? TotalWotravelTimeCost { get; set; }
        public decimal? TotalWosubcontractorCost { get; set; }
        public decimal? TotalWomaterialsCost { get; set; }
        public decimal? TotalWoexpensesCost { get; set; }
        public decimal? GrossMargin { get; set; }
        public DateTime? SlaResponsePenaltyDate { get; set; }
        public DateTime? SlaResolutionPenaltyDate { get; set; }
        public bool? MeetResponsePenaltySla { get; set; }
        public bool? MeetResolutionPenaltySla { get; set; }
        public int? ResponsePenaltyTime { get; set; }
        public int? ResolutionPenaltyTime { get; set; }

        public WorkOrders WorkOrders { get; set; }
    }
}
