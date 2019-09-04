using Group.Salto.Common;
using System;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class Expenses : BaseEntity
    {
        public int ExpenseTypeId { get; set; }
        public int PaymentMethodId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int ExpenseTicketId { get; set; }
        public double Factor { get; set; }

        public ExpensesTickets ExpenseTicket { get; set; }
        public ExpenseTypes ExpenseType { get; set; }
        public PaymentMethods PaymentMethod { get; set; }
    }
}
