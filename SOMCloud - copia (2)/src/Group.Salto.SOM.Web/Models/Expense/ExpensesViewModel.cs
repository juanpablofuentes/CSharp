using Group.Salto.Controls.Table.Models;
using System;

namespace Group.Salto.SOM.Web.Models.Expense
{
    public class ExpensesViewModel
    {
        public MultiItemViewModel<ExpenseViewModel, int> Expenses { get; set; }

        public ExpenseFilterViewModel ExpenseFilters { get; set; }
    }
}