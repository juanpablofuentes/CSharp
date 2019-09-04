using System;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Expenses
{
    public class ExpenseDto
    {
        public int Id { get; set; }
        public int ExpenseTypeId { get; set; }
        public string ExpenseType { get; set; }
        public int PaymentMethodId { get; set; }
        public string PaymentMethod { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string Date { get; set; }
        public double Factor { get; set; }
        public DateTime UpdateDate { get; set; }
        public int ExpenseTicketId { get; set; }
    }
}