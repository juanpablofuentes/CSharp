using System;

namespace Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderColumns
{
    public class WorkOrderSelectedViewColumnsDto : BaseWorkOrderColumns
    {
        public string FilterValues { get; set; }
        public DateTime? FilterStartDate { get; set; }
        public DateTime? FilterEndDate { get; set; }
        public string ToolTip { get; set; } = string.Empty;
    }
}