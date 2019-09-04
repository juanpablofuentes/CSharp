using System;
using System.Collections.Generic;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos;

namespace Group.Salto.ServiceLibrary.Common.Mobility.Dto.Tasks
{
    public class TaskExecutionValues
    {
        public WorkOrders CurrentWorkOrder { get; set; }
        public People CurrentPeople { get; set; }
        public Entities.Tenant.Tasks CurrentTask { get; set; }
        public TaskExecuteDto TaskParameters { get; set; }
        public ResultDto<bool> Result { get; set; }
        public IEnumerable<int> WoTypes { get; set; }
        public Services CreatedService { get; set; }
        public Guid CustomerId { get; set; }
    }
}
