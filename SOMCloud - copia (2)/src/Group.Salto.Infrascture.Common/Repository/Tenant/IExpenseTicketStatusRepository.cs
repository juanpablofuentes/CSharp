using Group.Salto.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IExpenseTicketStatusRepository : IRepository<ExpenseTicketStatus>
    {
        IQueryable<ExpenseTicketStatus> GetAll();
        Dictionary<Guid, string> GetAllStatus();
        ExpenseTicketStatus GetById(Guid id);
    }
}
