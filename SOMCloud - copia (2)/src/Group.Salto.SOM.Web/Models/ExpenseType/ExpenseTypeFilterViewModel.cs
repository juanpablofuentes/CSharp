using Group.Salto.Controls.Table.Models;
using System;

namespace Group.Salto.SOM.Web.Models.ExpenseType
{
    public class ExpenseTypeFilterViewModel : BaseFilter
    {
        public ExpenseTypeFilterViewModel()
        {
            OrderBy = "Type";
        }

        public string Type { get; set; }

    }
}