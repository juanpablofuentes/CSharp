using System;
using System.Linq;
using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class PurchaseRateRepository : BaseRepository<PurchaseRate>, IPurchaseRateRepository
    {
        public PurchaseRateRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<PurchaseRate> GetAll()
        {
            return Filter(x => !x.IsDeleted);
        }

        public PurchaseRate GetById(int id)
        {
            return Find(x => x.Id == id);
        }

        public SaveResult<PurchaseRate> CreatePurchaseRate(PurchaseRate purchaserate)
        {
            purchaserate.UpdateDate = DateTime.UtcNow;
            Create(purchaserate);
            var result = SaveChange(purchaserate);
            return result;
        }

        public SaveResult<PurchaseRate> UpdatePurchaseRate(PurchaseRate purchaserate)
        {
            purchaserate.UpdateDate = DateTime.UtcNow;
            Update(purchaserate);
            var result = SaveChange(purchaserate);
            return result;
        }

        public SaveResult<PurchaseRate> DeletePurchaseRate(PurchaseRate purchaserate)
        {
            purchaserate.UpdateDate = DateTime.UtcNow;
            Delete(purchaserate);
            SaveResult<PurchaseRate> result = SaveChange(purchaserate);
            result.Entity = purchaserate;
            return result;
        }
    }
}