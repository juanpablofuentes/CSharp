using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Microsoft.EntityFrameworkCore;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class ExpenseTicketRepository : BaseRepository<ExpensesTickets>, IExpenseTicketRepository
    {
        public ExpenseTicketRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<ExpensesTickets> GetAll()
        {
            return All();
        }

        public IQueryable<ExpensesTickets> GetAllWithIncludes()
        {
            return All()
                .Include(x=>x.People)
                .ThenInclude(x=>x.Company)
                .Include(x=>x.Expenses);
        }

        public IQueryable<ExpensesTickets> GetByPeopleId(int peopleId)
        {
            return Filter(x => x.PeopleId == peopleId);
        }

        public ExpensesTickets GetById(int id)
        {
            return Find(x => x.Id == id);
        }

        public ExpensesTickets GetByIdIncludeSomFile(int id)
        {
            return Filter(x => x.Id == id)
                .Include(x=>x.PeopleValidator)
                .Include(x => x.Expenses)
                .Include(x => x.ExpensesTicketFile)
                .ThenInclude(x => x.SomFile).FirstOrDefault();
        }

        public IQueryable<ExpensesTickets> FilterById(IEnumerable<int> ids)
        {
            return Filter(x => ids.Any(k => k == x.Id));
        }

        public SaveResult<ExpensesTickets> CreateExpensesTicket(ExpensesTickets expensesTicket)
        {
            expensesTicket.UpdateDate = DateTime.UtcNow;
            Create(expensesTicket);
            var result = SaveChange(expensesTicket);
            return result;
        }

        public SaveResult<ExpensesTickets> UpdateExpensesTicket(ExpensesTickets expensesTicket)
        {
            expensesTicket.UpdateDate = DateTime.UtcNow;
            Update(expensesTicket);
            var result = SaveChange(expensesTicket);
            return result;
        }

        public SaveResult<ExpensesTickets> DeleteExpensesTicket(ExpensesTickets expensesTicket)
        {
            expensesTicket.UpdateDate = DateTime.UtcNow;
            Delete(expensesTicket);
            SaveResult<ExpensesTickets> result = SaveChange(expensesTicket);
            result.Entity = expensesTicket;
            return result;
        }

        public Dictionary<int, string> GetAllPeopleKeyValues()
        {
            return All()
                .OrderBy(o => o.People.Name)
                .Select(o=>o.People)
                .Distinct()
                .ToDictionary(t => t.Id, t => t.Name +" "+ t.FisrtSurname+" "+ t.SecondSurname);
        }

        public Dictionary<int, string> GetAllStatesKeyValues()
        {
            return All()
                .GroupBy(o => o.Status)
                .Select(x=>x.First())
                .ToDictionary(t => t.Id, t => t.Status);
        }
    }
}