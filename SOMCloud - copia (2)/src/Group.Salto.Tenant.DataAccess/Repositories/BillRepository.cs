using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class BillRepository : BaseRepository<Bill>, IBillRepository
    {
        public BillRepository(ITenantUnitOfWork uow) : base(uow) { }

        public List<Bill> GetAllByWorkOrderId(int id)
        {
            IQueryable<Bill> query = Filter(x => x.WorkorderId == id)
                                    .Include(x => x.BillLine);
            return query.ToList();
        }
        public IQueryable<Bill> GetAll()
        {
            return All().Include(x => x.People)
                        .Include(x => x.Workorder)
                            .ThenInclude(y => y.Project)
                        .Include(x => x.Workorder.Asset.AssetStatus)
                        .Include(x => x.BillLine);
        }

        public int CountAll()
        {
            return All().Count();
        }

        public Bill GetById(int id)
        {
            return Filter(x => x.Id == id)
                        .Include(x => x.People)
                        .Include(x => x.Workorder)
                        .Include(x => x.BillLine)
                        .SingleOrDefault();
        }
    }
}