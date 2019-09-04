using System.Linq;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IPurchaseRateRepository : IRepository<PurchaseRate>
    {
        IQueryable<PurchaseRate> GetAll();
        PurchaseRate GetById(int id);
        SaveResult<PurchaseRate> CreatePurchaseRate(PurchaseRate purchaserate);
        SaveResult<PurchaseRate> UpdatePurchaseRate(PurchaseRate purchaserate);
        SaveResult<PurchaseRate> DeletePurchaseRate(PurchaseRate purchaserate);
    }
}