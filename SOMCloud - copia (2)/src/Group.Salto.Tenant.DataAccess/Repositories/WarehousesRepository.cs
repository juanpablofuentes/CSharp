using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class WarehousesRepository : BaseRepository<Warehouses>, IWarehousesRepository
    {
        public WarehousesRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public SaveResult<Warehouses> CreateWarehouse(Warehouses warehouse)
        {
            warehouse.UpdateDate = DateTime.UtcNow;
            Create(warehouse);
            var result = SaveChange(warehouse);
            return result; 
        }

        public SaveResult<Warehouses> DeleteWarehouse(Warehouses warehouse)
        {
            warehouse.UpdateDate = DateTime.UtcNow;
            Delete(warehouse);
            SaveResult<Warehouses> result = SaveChange(warehouse);
            result.Entity = warehouse;
            return result;  
        }

        public IQueryable<Warehouses> GetAll() 
        {
            return All();
        }

        public Warehouses GetById(int Id)
        {
            return Filter(x => x.Id == Id).SingleOrDefault();
        }

        public Warehouses GetByIdCanDelete(int id)
        {
            return Filter(p => p.Id == id)
                    .Include(x => x.People)
                    .Include(x => x.WarehouseMovementEndpoints)
                        .ThenInclude(x => x.WarehouseMovementsFrom)
                    .Include(y => y.WarehouseMovementEndpoints)
                        .ThenInclude(x => x.WarehouseMovementsTo)
                    .SingleOrDefault(); 
        }

        public Warehouses GetByIdIncludeReferencesToDelete(int id)
        {
             return Filter(p => p.Id == id)
                    .Include(x => x.WarehouseMovementEndpoints)
                    .SingleOrDefault();
        }

        public SaveResult<Warehouses> UpdateWarehouse(Warehouses warehouse) 
        {
            warehouse.UpdateDate = DateTime.UtcNow;
            Update(warehouse);
            var result = SaveChange(warehouse);
            return result;        
        }
    }
}