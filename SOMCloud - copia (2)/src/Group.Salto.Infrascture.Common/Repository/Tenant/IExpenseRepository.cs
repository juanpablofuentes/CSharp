using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IExpenseRepository
    {
        IQueryable<Expenses> GetByExpenseTicketId(int expenseticketId);
        IQueryable<Expenses> GetAll();
        SaveResult<Expenses> CreateExpense(Expenses expense);
        SaveResult<Expenses> DeleteExpense(Expenses expense);
    }
}