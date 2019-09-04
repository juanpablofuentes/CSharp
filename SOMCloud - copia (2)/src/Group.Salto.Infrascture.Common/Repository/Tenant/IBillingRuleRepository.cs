using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IBillingRuleRepository : IRepository<BillingRule>
    {
        IQueryable<BillingRule> GetAllByTaskId(int id);
        BillingRule GetById(int id);
        SaveResult<BillingRule> CreateBillingRule(BillingRule entity);
        SaveResult<BillingRule> UpdateBillingRule(BillingRule entity);
        SaveResult<BillingRule> DeleteBillingRule(BillingRule entity);
    }
}