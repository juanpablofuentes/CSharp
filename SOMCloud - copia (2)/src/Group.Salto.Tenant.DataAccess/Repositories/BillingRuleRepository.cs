using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class BillingRuleRepository: BaseRepository<BillingRule>, IBillingRuleRepository
    {
        public BillingRuleRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<BillingRule> GetAllByTaskId(int id)
        {
            return Filter(x => x.TaskId == id)
                        .Include(x => x.BillingRuleItem)
                            .ThenInclude(x => x.Item)
                        .Include(x => x.ErpSystemInstance);
        }

        public BillingRule GetById(int id) 
        {
            return Filter(x => x.Id == id).FirstOrDefault();
        }

        public SaveResult<BillingRule> CreateBillingRule(BillingRule entity) 
        {
            entity.UpdateDate = DateTime.UtcNow;
            Create(entity);
            var result = SaveChange(entity);
            return result;
        }

        public SaveResult<BillingRule> UpdateBillingRule(BillingRule entity) 
        {
            entity.UpdateDate = DateTime.UtcNow;
            Update(entity);
            var result = SaveChange(entity);
            return result;
        }

        public SaveResult<BillingRule> DeleteBillingRule(BillingRule entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Delete(entity);
            SaveResult<BillingRule> result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }
    }
}