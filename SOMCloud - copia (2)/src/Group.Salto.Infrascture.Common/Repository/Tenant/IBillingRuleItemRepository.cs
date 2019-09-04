using Group.Salto.Common;
using Group.Salto.Entities.Tenant;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IBillingRuleItemRepository : IRepository<BillingRuleItem>
    {
        BillingRuleItem GetById(int id);
        SaveResult<BillingRuleItem> CreateBillingRuleItem(BillingRuleItem entity);
        SaveResult<BillingRuleItem> UpdateBillingRuleItem(BillingRuleItem entity);
        SaveResult<BillingRuleItem> DeleteBillingRuleItem(BillingRuleItem entity);
    }
}