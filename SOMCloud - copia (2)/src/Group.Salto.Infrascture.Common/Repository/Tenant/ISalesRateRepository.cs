using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface ISalesRateRepository : IRepository<SalesRate>
    {
        SalesRate GetById(int id);
        IQueryable<SalesRate> GetAllNotDeleted();
        SalesRate GetByIdNotDeleted(int id);
        SaveResult<SalesRate> CreateSalesRate(SalesRate salerate);
        SaveResult<SalesRate> UpdateSalesRate(SalesRate salerate);
        SaveResult<SalesRate> DeleteSalesRate(SalesRate salerate);
    }
}