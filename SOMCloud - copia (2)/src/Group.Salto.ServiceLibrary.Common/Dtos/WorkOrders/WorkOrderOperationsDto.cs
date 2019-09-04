using Group.Salto.ServiceLibrary.Common.Dtos.ServiceAnalysis;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderAnalysis;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders
{
    public class WorkOrderOperationsDto
    {
        public WorkOrderOperationsDto()
        {
            Services = new List<ServiceAnalysisDto>();
        }
        public List<ServiceAnalysisDto> Services { get; set; }
        public WOAnalysisDto WOAnalysis { get; set; }
        public ServiceAnalysisDto TotalService { get; set; }
    }
}