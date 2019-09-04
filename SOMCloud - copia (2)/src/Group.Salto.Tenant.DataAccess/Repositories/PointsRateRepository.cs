using System.Linq;
using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class PointsRateRepository : BaseRepository<PointsRate>, IPointsRateRepository
    {
        public PointsRateRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        IQueryable<PointsRate> IPointsRateRepository.GetAll()
        {
            return All();
        }
    }
}