using Group.Salto.Common;
using System;

namespace Group.Salto.Entities.Tenant
{
    public class ExpensesTicketsFiles : BaseEntity<Guid>
    {
        public int ExpenseTicketId { get; set; }
        public ExpensesTickets ExpenseTicket { get; set; }
    }
}