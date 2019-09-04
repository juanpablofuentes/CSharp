using Group.Salto.ServiceLibrary.Common.Dtos.Grid;
using System;

namespace Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders
{
    public class WorkOrderFiltersDto : GridBaseFiltersDto
    {
        public string WorkOrderId { get; set; }
        public string InternalIdentifier { get; set; }
        public string SerialNumber { get; set; }
        public string LocationCode { get; set; }
        public DateTime? ResolutionDateSla { get; set; }
        public DateTime? CreationStartDate { get; set; }
        public DateTime? CreationEndDate { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ActionDateStartDate { get; set; }
        public DateTime? ActionDateEndDate { get; set; }
        public DateTime? ActionDateDate { get; set; }
        public string WorkOrderStatusIds { get; set; }
        public string WorkOrderQueueIds { get; set; }
        public string ProjectIds { get; set; }
        public string WorkOrderCategoryIds { get; set; }
        public string StateIds { get; set; }
        public string ResponsiblesIds { get; set; }
        public WorkOrderSearch WorkOrderSearch { get; set; }
        public string WorkOrderType { get; set; }
        public string InsertedBy { get; set; }
        public string Manipulator { get; set; }
        public string SaltoClient { get; set; }
        public string EndClient { get; set; }
        public string Zone { get; set; }
        public string ExternalWorOrderStatus { get; set; }
        public DateTime? AssignmentTimeStartDate { get; set; }
        public DateTime? PickUpTimeStartDate { get; set; }
        public DateTime? FinalClientClosingTimeStartDate { get; set; }
        public DateTime? InternalClosingTimeStartDate { get; set; }
        public DateTime? AssignmentTimeEndDate { get; set; }
        public DateTime? PickUpTimeEndDate { get; set; }
        public DateTime? FinalClientClosingTimeEndDate { get; set; }
        public DateTime? InternalClosingTimeEndDate { get; set; }
    }
}