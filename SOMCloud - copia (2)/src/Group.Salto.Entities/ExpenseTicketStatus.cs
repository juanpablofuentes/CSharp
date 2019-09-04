using Group.Salto.Common;
using System;

namespace Group.Salto.Entities
{
    public class ExpenseTicketStatus : BaseEntity<Guid>
    {
        public string Description { get; set; }
    }
}