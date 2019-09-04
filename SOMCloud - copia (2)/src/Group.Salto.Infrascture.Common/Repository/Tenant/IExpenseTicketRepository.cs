using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IExpenseTicketRepository : IRepository<ExpensesTickets>
    {
        IQueryable<ExpensesTickets> GetAll();
        IQueryable<ExpensesTickets> GetAllWithIncludes();
        IQueryable<ExpensesTickets> GetByPeopleId(int peopleId);
        ExpensesTickets GetById(int id);
        ExpensesTickets GetByIdIncludeSomFile(int id);
        IQueryable<ExpensesTickets> FilterById(IEnumerable<int> ids);
        SaveResult<ExpensesTickets> CreateExpensesTicket(ExpensesTickets expensesTicket);
        SaveResult<ExpensesTickets> UpdateExpensesTicket(ExpensesTickets expensesTicket);
        SaveResult<ExpensesTickets> DeleteExpensesTicket(ExpensesTickets expensesTicket);
        Dictionary<int, string> GetAllPeopleKeyValues();
        Dictionary<int, string> GetAllStatesKeyValues();
    }
}
