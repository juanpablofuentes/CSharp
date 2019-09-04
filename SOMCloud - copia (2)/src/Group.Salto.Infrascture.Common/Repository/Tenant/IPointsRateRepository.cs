using Group.Salto.Entities.Tenant;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IPointsRateRepository
    {
        IQueryable<PointsRate> GetAll(); 
    }
}