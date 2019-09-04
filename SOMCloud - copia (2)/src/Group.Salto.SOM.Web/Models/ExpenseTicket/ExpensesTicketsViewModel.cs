using Group.Salto.Controls.Table.Models;
using Group.Salto.SOM.Web.Models.Expense;
using Group.Salto.SOM.Web.Models.Vehicles;
using System;

namespace Group.Salto.SOM.Web.Models.ExpenseTicket
{
    public class ExpensesTicketsViewModel
    {
        public MultiItemViewModel<ExpenseTicketViewModel, int> ExpenseTickets { get; set; }
        public ExpenseTicketFilterViewModel ExpenseTicketFilters { get; set; }
        public MultiItemViewModel<ExpenseDetailsViewModel,int> ExpenseModal { get; set; }
    }
}