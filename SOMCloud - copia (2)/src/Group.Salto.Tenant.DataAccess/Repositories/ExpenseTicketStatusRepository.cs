using DataAccess.Common;
using Group.Salto.Entities;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class ExpenseTicketStatusRepository : BaseRepository<ExpenseTicketStatus>, IExpenseTicketStatusRepository
    {
        public ExpenseTicketStatusRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<ExpenseTicketStatus> GetAll()
        {
            return All();
        }

        public Dictionary<Guid, string> GetAllStatus()
        {
            return All()
                .ToDictionary(t => t.Id, t => t.Description);
        }

        public ExpenseTicketStatus GetById(Guid id)
        {
            return Find(x => x.Id == id);
        }
    }
}
