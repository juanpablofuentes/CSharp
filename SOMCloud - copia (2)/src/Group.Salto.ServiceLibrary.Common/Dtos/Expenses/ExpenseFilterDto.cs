using Group.Salto.Controls.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Expenses
{
    public class ExpenseFilterDto : ISortableFilter
    {
        public string OrderBy { get; set; }
        public bool Asc { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    
    }
}