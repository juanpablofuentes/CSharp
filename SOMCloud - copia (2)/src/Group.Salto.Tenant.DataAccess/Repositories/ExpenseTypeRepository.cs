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
    public class ExpenseTypeRepository : BaseRepository<ExpenseTypes>, IExpenseTypeRepository
    {
        public ExpenseTypeRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<ExpenseTypes> GetAll()
        {
            return All();
        }

        public ExpenseTypes GetById(int id)
        {
            return Filter(x => x.Id == id).Include(x => x.Expenses).FirstOrDefault();
        }

        public SaveResult<ExpenseTypes> CreateExpenseType(ExpenseTypes entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Create(entity);
            var result = SaveChange(entity);
            return result;
        }

        public SaveResult<ExpenseTypes> DeleteExpenseType(ExpenseTypes entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Delete(entity);
            SaveResult<ExpenseTypes> result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }

        public SaveResult<ExpenseTypes> UpdateExpenseType(ExpenseTypes entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Update(entity);
            var result = SaveChange(entity);
            return result;
        }

        public Dictionary<int, string> GetExpenseTypeKeyValues()
        {
            return All()
                .OrderBy(o => o.Type)
                .ToDictionary(t => t.Id, t => t.Type);
        }
    }
}