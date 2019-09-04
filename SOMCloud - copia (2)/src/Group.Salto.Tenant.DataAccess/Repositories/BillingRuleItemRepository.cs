using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using System;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class BillingRuleItemRepository : BaseRepository<BillingRuleItem>, IBillingRuleItemRepository
    {
        public BillingRuleItemRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public BillingRuleItem GetById(int id) 
        {
            return Filter(x => x.Id == id).FirstOrDefault();
        }

        public SaveResult<BillingRuleItem> CreateBillingRuleItem(BillingRuleItem entity) 
        {
            entity.UpdateDate = DateTime.UtcNow;
            Create(entity);
            var result = SaveChange(entity);
            return result;
        }

        public SaveResult<BillingRuleItem> UpdateBillingRuleItem(BillingRuleItem entity) 
        {
            entity.UpdateDate = DateTime.UtcNow;
            Update(entity);
            var result = SaveChange(entity);
            return result;
        }

        public SaveResult<BillingRuleItem> DeleteBillingRuleItem(BillingRuleItem entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Delete(entity);
            SaveResult<BillingRuleItem> result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }
    }
}