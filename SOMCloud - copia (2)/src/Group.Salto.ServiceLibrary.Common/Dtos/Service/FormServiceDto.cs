using System;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Service
{
    public class FormServiceDto
    {
        public int TaskId { get; set; }
        public int WorkOrderId { get; set; }
        public int ServiceId { get; set; }
        public int UserId { get; set; }
        public Guid CustomerId { get; set; }
    }
}