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
    public class ExpenseTicketFileRepository : BaseRepository<ExpensesTicketFile>, IExpenseTicketFileRepository
    {
        public ExpenseTicketFileRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public ExpensesTicketFile GetByExpenseTicketId(int id)
        {
            return Filter(x => x.ExpenseTicketId == id).Include(x=>x.SomFile).FirstOrDefault();
        }

        public SaveResult<ExpensesTicketFile> DeleteExpenseTicketFile(ExpensesTicketFile entity)
        {
            Delete(entity);
            SaveResult<ExpensesTicketFile> result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }
    }
}