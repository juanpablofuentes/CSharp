using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IPointRateRepository
    {
        IQueryable<PointsRate> GetAll();
        PointsRate GetById(int id);
        SaveResult<PointsRate> CreatePointsRate(PointsRate vehicle);
        SaveResult<PointsRate> UpdatePointsRate(PointsRate vehicle);
        SaveResult<PointsRate> DeletePointsRate(PointsRate vehicle);
        IQueryable<PointsRate> FilterById(IEnumerable<int> ids);
        Dictionary<int, string> GetAllKeyValues();
        PointsRate GetByIdCanDelete(int id);
        bool CheckNameExists(string pointRateName);
    }
}