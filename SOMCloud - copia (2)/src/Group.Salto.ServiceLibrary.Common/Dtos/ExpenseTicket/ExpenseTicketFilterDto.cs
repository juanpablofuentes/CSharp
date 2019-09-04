using Group.Salto.Controls.Entities;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.ExpenseTicket
{
    public class ExpenseTicketFilterDto : BaseFilterDto
    {
        public IList<int> NamePeople { get; set; }
        public DateTime? InitialDate { get; set; }
        public DateTime? FinalDate { get; set; }
        public IList<Guid?> States { get; set; }
    }
}