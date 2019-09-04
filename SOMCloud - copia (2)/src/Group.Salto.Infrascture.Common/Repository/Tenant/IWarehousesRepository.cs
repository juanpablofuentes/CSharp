using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IWarehousesRepository : IRepository<Warehouses>
    {
        IQueryable<Warehouses> GetAll();   
        Warehouses GetById(int Id);   
        SaveResult<Warehouses> CreateWarehouse(Warehouses warehouse);
        SaveResult<Warehouses> UpdateWarehouse(Warehouses warehouse);
        Warehouses GetByIdIncludeReferencesToDelete(int id);
        Warehouses GetByIdCanDelete(int id);
        SaveResult<Warehouses> DeleteWarehouse(Warehouses warehouse);
    }
}