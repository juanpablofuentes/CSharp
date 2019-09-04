using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IExpenseTicketFileRepository
    {
        ExpensesTicketFile GetByExpenseTicketId(int id);
        SaveResult<ExpensesTicketFile> DeleteExpenseTicketFile(ExpensesTicketFile entity);
    }
}