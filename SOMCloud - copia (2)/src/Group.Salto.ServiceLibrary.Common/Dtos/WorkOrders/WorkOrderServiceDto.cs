using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders
{
    public class WorkOrderServiceDto
    {
        public WorkOrderServiceDto()
        {
            WOService = new List<WorkOrderFormsDto>();
        }
        public List<WorkOrderFormsDto> WOService { get; set; }
    }
}