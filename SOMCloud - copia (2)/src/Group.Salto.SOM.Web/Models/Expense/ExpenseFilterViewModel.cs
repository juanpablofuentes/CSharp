using Group.Salto.Controls.Table.Models;
using System;

namespace Group.Salto.SOM.Web.Models.Expense
{
    public class ExpenseFilterViewModel : BaseFilter
    {
        public ExpenseFilterViewModel()
        {
            OrderBy = "Description";
        }

        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}