using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IExpenseTypeRepository : IRepository<ExpenseTypes>
    {
        IQueryable<ExpenseTypes> GetAll();
        ExpenseTypes GetById(int id);
        SaveResult<ExpenseTypes> CreateExpenseType(ExpenseTypes entity);
        SaveResult<ExpenseTypes> DeleteExpenseType(ExpenseTypes entity);
        SaveResult<ExpenseTypes> UpdateExpenseType(ExpenseTypes entity);
        Dictionary<int, string> GetExpenseTypeKeyValues();
    }
}