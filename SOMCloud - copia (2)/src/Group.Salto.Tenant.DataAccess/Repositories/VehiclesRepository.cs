using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class VehiclesRepository : BaseRepository<Vehicles>, IVehiclesRepository
    {
        public VehiclesRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public SaveResult<Vehicles> DeleteVehicle(Vehicles vehicle)
        {
            vehicle.UpdateDate = DateTime.UtcNow;
            Delete(vehicle);
            SaveResult<Vehicles> result = SaveChange(vehicle);
            result.Entity = vehicle;
            return result;
        }

        public Vehicles GetById(int id)
        {
            return Find(x => x.Id == id);
        }

        public SaveResult<Vehicles> UpdateVehicle(Vehicles vehicle)
        {
            vehicle.UpdateDate = DateTime.UtcNow;
            Update(vehicle);
            var result = SaveChange(vehicle);
            return result;
        }

        public IQueryable<Vehicles> GetAllNotDeleted()
        {
            return Filter(v => !v.IsDeleted, GetIncludeDriver());
        }

        public SaveResult<Vehicles> CreateVehicle(Vehicles vehicle)
        {
            vehicle.UpdateDate = DateTime.UtcNow;
            Create(vehicle);
            var result = SaveChange(vehicle);
            return result;
        }

        public Dictionary<int, string> GetAllKeyValues()
        {
            return All()
                .Where(s => !s.IsDeleted)
                .OrderBy(o => o.Name)
                .ToDictionary(t => t.Id, t => t.Name);
        }

        public IQueryable<Vehicles> GetByPeopleId(int peopleId)
        {
            return Filter(v => !v.IsDeleted && v.PeopleDriverId == peopleId);
        }

        private List<Expression<Func<Vehicles, object>>> GetIncludeDriver()
        {
            return GetIncludesPredicate(new List<Type>() { typeof(People) });
        }

        private List<Expression<Func<Vehicles, object>>> GetIncludesPredicate(IList<Type> includeCollection)
        {
            var includesPredicate = new List<Expression<Func<Vehicles, object>>>();
            foreach (var element in includeCollection)
            {
                if (element == typeof(People))
                {
                    includesPredicate.Add(p => p.PeopleDriver);
                }
            }
            return includesPredicate;
        }
    }
}