using Group.Salto.Controls.Table.Models;
using Group.Salto.SOM.Web.Models.MultiCombo;
using System;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.ExpenseTicket
{
    public class ExpenseTicketFilterViewModel : BaseFilter
    {
        public ExpenseTicketFilterViewModel()
        {
            OrderBy = "InitialDate";
        }

        
        public DateTime? InitialDate { get; set; }
        public DateTime? FinalDate { get; set; }
        public IList<MultiComboViewModel<int, string>> NamePeople { get; set; }
        public IList<MultiComboViewModel<Guid?, string>> States { get; set; }
    }
}