using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders;
using System;

namespace Group.Salto.ServiceLibrary.Common.Mobility.Dto.Tasks
{
    public class TaskExecuteFormDto
    {
        public WorkOrderFormsDto Service { get; set; }
        public int WorkOrderId { get; set; }
        public int ServiceId { get; set; }
        public TaskTypeEnum Type { get; set; }
        public int UserId { get; set; }
        public Guid CustomerId { get; set; }
    }
}