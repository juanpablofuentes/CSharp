using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class PointRateRepository : BaseRepository<PointsRate>, IPointRateRepository
    {
        public PointRateRepository(ITenantUnitOfWork uow) : base(uow) { }
       
        public IQueryable<PointsRate> FilterById(IEnumerable<int> ids)
        {
            return Filter(x => ids.Any(k => k == x.Id));
        }

        public IQueryable<PointsRate> GetAll()
        {
            return All();
        }

        public PointsRate GetById(int id)
        {
            return Find(x => x.Id == id);
        }

        public PointsRate GetByIdCanDelete(int id)
        {
            return Filter(x => x.Id == id)
                .Include(x=>x.ItemsPointsRate)
                .Include(x=>x.People).SingleOrDefault();
        }

        public SaveResult<PointsRate> UpdatePointsRate(PointsRate pointsrate)
        {
            pointsrate.UpdateDate = DateTime.UtcNow;
            Update(pointsrate);
            var result = SaveChange(pointsrate);
            return result;
        }

        public SaveResult<PointsRate> CreatePointsRate(PointsRate pointsrate)
        {
            pointsrate.UpdateDate = DateTime.UtcNow;
            Create(pointsrate);
            var result = SaveChange(pointsrate);
            return result;
        }

        public SaveResult<PointsRate> DeletePointsRate(PointsRate pointsrate)
        {
            pointsrate.UpdateDate = DateTime.UtcNow;
            Delete(pointsrate);
            SaveResult<PointsRate> result = SaveChange(pointsrate);
            result.Entity = pointsrate;
            return result;
        }

        public Dictionary<int, string> GetAllKeyValues()
        {
            return All()
                .Select(s => new { s.Id, s.Name })
                .OrderBy(o => o.Name)
                .ToDictionary(t => t.Id, t => t.Name);
        }

        public bool CheckNameExists(string pointRateName)
        {
            return Filter(x => x.Name.ToLower().Trim() == pointRateName.ToLower().Trim()).Any();
        }
    }
}