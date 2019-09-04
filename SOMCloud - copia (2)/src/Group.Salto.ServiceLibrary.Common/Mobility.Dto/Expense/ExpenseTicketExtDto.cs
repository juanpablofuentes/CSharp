using System;
using Group.Salto.ServiceLibrary.Common.Dtos.People;

namespace Group.Salto.ServiceLibrary.Common.Mobility.Dto.Expense
{
    public class ExpenseTicketExtDto
    {
        public int Id { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime Date { get; set; }
        public int? WorkOrderId { get; set; }
        public Guid? Status { get; set; }
        public PeopleDto PeopleValidator { get; set; }
        public DateTime? ValidationDate { get; set; }
        public int validatorPeopleId { get; set; }
        public string ValidationObservations { get; set; }
        public string PaymentInformation { get; set; }
        public int ExpenseId { get; set; }
        public int ExpenseTypeId { get; set; }
        public int PaymentMethodId { get; set; }
        public string Description { get; set; }
        public string StatusExpense { get; set; }
        public decimal Amount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public double Factor { get; set; }
        public DateTime ExpenseUpdateDate { get; set; }
    }
}
