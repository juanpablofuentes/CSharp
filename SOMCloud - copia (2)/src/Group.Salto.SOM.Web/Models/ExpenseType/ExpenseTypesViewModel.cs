using Group.Salto.Controls.Table.Models;
using Group.Salto.SOM.Web.Models.ExpenseType;
using System;

namespace Group.Salto.SOM.Web.Models.ExpenseType
{
    public class ExpenseTypesViewModel
    {
        public MultiItemViewModel<ExpenseTypeViewModel, int> ExpenseType { get; set; }
        public ExpenseTypeFilterViewModel ExpenseTypeFilters { get; set; }
    }
}