using Group.Salto.Common;
using System;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class ExpensesTickets : BaseEntity
    {
        public DateTime Date { get; set; }
        public int PeopleId { get; set; }
        public int? WorkOrderId { get; set; }
        public string Status { get; set; }
        public string UniqueId { get; set; }
        public int? PeopleValidatorId { get; set; }
        public DateTime? ValidationDate { get; set; }
        public string ValidationObservations { get; set; }
        public string PaymentInformation { get; set; }

        public People People { get; set; }
        public People PeopleValidator { get; set; }
        public WorkOrders WorkOrder { get; set; }
        public Guid? ExpenseTicketStatusId { get; set; }
        public ICollection<Expenses> Expenses { get; set; }
        public ICollection<ExpensesTicketFile> ExpensesTicketFile { get; set; }
        public ICollection<ExpensesTicketsFiles> ExpensesTicketsFiles { get; set; }
    }
}
