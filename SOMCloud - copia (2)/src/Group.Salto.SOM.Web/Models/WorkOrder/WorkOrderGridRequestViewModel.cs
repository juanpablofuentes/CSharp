using Group.Salto.SOM.Web.Models.Grid;
using System;

namespace Group.Salto.SOM.Web.Models.WorkOrder
{
    public class WorkOrderGridRequestViewModel : BaseGridViewModel
    {
        public WorkOrderGridRequestViewModel()
        {
            WorkOrderSearch = new WorkOrderSearch();
        }

        public int Dhx_no_header { get; set; }
        public int UserId { get; set; }
        public int LanguageId { get; set; }
        public string WorkOrderId { get; set; }
        public string InternalIdentifier { get; set; }
        public string SerialNumber { get; set; }
        public string LocationCode { get; set; }
        public string ResolutionDateSla { get; set; }
        public string CreationStartDate { get; set; }
        public string CreationEndDate { get; set; }
        public string CreationDate { get; set; }
        public string ActionDateStartDate { get; set; }
        public string ActionDateEndDate { get; set; }
        public string ActionDateDate { get; set; }
        public string WorkOrderStatus { get; set; }
        public string WorkOrderQueue { get; set; }
        public string ProjectIds { get; set; }
        public string WorkOrderCategoryIds { get; set; }
        public string StateIds { get; set; }
        public string ResponsiblesIds { get; set; }
        public WorkOrderSearch WorkOrderSearch { get; set; }
        public bool ExportToExcel { get; set; }
        public bool ExportAllToExcel { get; set; }
        public int ConfigurationViewId { get; set; }
        public bool IsQuickFilter { get; set; }
    }
}