using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class ExpenseRepository: BaseRepository<Expenses>, IExpenseRepository
    {
        public ExpenseRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<Expenses> GetByExpenseTicketId(int expenseticketId)
        {
            return Filter(x => x.ExpenseTicketId == expenseticketId);
        }

        public IQueryable<Expenses> GetAll()
        {
            return All()
                .Include(x => x.PaymentMethod)
                .Include(x => x.ExpenseType);
        }

        public SaveResult<Expenses> CreateExpense(Expenses expense)
        {
            expense.UpdateDate = DateTime.UtcNow;
            Create(expense);
            var result = SaveChange(expense);
            return result;
        }

        public SaveResult<Expenses> DeleteExpense(Expenses expense)
        {
            expense.UpdateDate = DateTime.UtcNow;
            Delete(expense);
            SaveResult<Expenses> result = SaveChange(expense);
            result.Entity = expense;
            return result;
        }
    }
}