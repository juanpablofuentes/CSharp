using System;

namespace Group.Salto.Entities
{
    public class CustomerModule
    {
        public Customers Customer { get; set; }
        public Guid CustomerId { get; set; }
        public Module Module { get; set; }
        public Guid ModuleId { get; set; }
    }
}
