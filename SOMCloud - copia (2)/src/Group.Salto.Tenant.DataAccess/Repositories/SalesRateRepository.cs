using System;
using System.Linq;
using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class SalesRateRepository : BaseRepository<SalesRate>, ISalesRateRepository
    {
        public SalesRateRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public SalesRate GetById(int id)
        {
            return Find(x => x.Id == id);
        }

        public SaveResult<SalesRate> CreateSalesRate(SalesRate salerate)
        {
            salerate.UpdateDate = DateTime.UtcNow;
            Create(salerate);
            var result = SaveChange(salerate);
            return result;
        }

        public SaveResult<SalesRate> DeleteSalesRate(SalesRate salerate)
        {
            salerate.UpdateDate = DateTime.UtcNow;
            Delete(salerate);
            SaveResult<SalesRate> result = SaveChange(salerate);
            result.Entity = salerate;
            return result;
        }

        public IQueryable<SalesRate> GetAllNotDeleted()
        {
            return Filter(x => !x.IsDeleted);
        }

        public SalesRate GetByIdNotDeleted(int id)
        {
            return Find(x=>x.Id == id && !x.IsDeleted);
        }

        public SaveResult<SalesRate> UpdateSalesRate(SalesRate salerate)
        {
            salerate.UpdateDate = DateTime.UtcNow;
            Update(salerate);
            var result = SaveChange(salerate);
            return result;
        }
    }
}