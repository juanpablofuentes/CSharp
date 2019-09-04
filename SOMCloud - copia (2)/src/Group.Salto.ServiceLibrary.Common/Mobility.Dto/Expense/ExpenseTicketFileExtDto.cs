using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Mobility.Dto.Expense
{
    public class ExpenseTicketFileExtDto : ExpenseTicketExtDto
    {
       public FileContentDto ExpenseFile { get; set; }
    }
}