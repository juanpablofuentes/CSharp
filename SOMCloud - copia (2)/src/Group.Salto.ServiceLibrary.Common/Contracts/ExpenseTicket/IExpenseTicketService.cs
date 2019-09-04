using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Expenses;
using Group.Salto.ServiceLibrary.Common.Dtos.ExpenseTicket;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Contracts.ExpenseTicket
{
    public interface IExpenseTicketService
    {
        IList<BaseNameIdDto<int>> Filter(QueryRequestDto queryRequest);
        ResultDto<IList<ExpenseTicketDto>> GetAllFiltered(ExpenseTicketFilterDto filter);
    }
}