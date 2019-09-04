using Group.Salto.ServiceLibrary.Common.Dtos.People;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Expenses
{
    public class ExpenseTicketDto
    {
        public int Id { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Date { get; set; }
        public int? WorkOrderId { get; set; }
        public Guid? ExpenseTicketStatusId { get; set; }
        public string Status { get; set; }
        public PeopleDto PeopleValidator { get; set; }
        public PeopleDto People { get; set; }
        public string ValidationDate { get; set; }
        public string ValidationObservations { get; set; }
        public string PaymentInformation { get; set; }
        public IEnumerable<ExpenseDto> TicketExpenses { get; set; }
    }
}