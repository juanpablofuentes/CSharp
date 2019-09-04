using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class BillLineRepository : BaseRepository<BillLine>, IBillLineRepository
    {
        public BillLineRepository(ITenantUnitOfWork uow) : base(uow) { }

        public List<BillLine> GetAllByBillId(int id)
        {
            IQueryable<BillLine> query = Filter(x => x.BillId == id)
                                        .Include(x => x.Item);
            return query.ToList();
        }
    }
}