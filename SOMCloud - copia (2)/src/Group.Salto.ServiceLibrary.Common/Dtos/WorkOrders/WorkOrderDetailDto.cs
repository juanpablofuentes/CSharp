using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders
{
    public class WorkOrderDetailDto
    {
        public WorkOrderDetailDto()
        {
            WorkOrderTypes = new List<string>();
        }

        public int Id { get; set; }
        public string InternalIdentifier { get; set; }
        public string ActionDate { get; set; }
        public string ResolutionDateSla { get; set; }
        public string TextRepair { get; set; }
        public string Observations { get; set; }
        public string WorkOrderStatus { get; set; }
        public string ExternalWorOrderStatus { get; set; }
        public string Queue { get; set; }
        public string Project { get; set; }
        public string WorkOrderCategory { get; set; }
        public string WorkOrderCategoryURL { get; set; }
        public List<string> WorkOrderTypes { get; set; }
        public string FinalClient { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public string LocationPhone { get; set; }
        public string LocationAddress { get; set; }
        public string PeopleResponsibleName { get; set; }
        public string PeopleResponsiblePhone { get; set; }
        public string BrandURL { get; set; }
        public int? ExpectedWorkedTime { get; set; }
        public int? TotalWorkedTime { get; set; }
        public decimal? ExpectedVSTotalTime { get; set; }
        public decimal? Km { get; set; }
        public int? TravelTime { get; set; }
        public int? WaitTime { get; set; }
        public int? OnSiteTime { get; set; }
        public bool ShowHeader { get; set; }
        public string StatusColor { get; set; }
        public int? AssetId { get; set; }
        public int? FatherId { get; set; }
        public bool ReferenceOtherServices { get; set; }
        public int? GeneratedServiceId { get; set; }
        public bool GeneratedService { get; set; }
    }
}