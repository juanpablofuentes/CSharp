using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IWarehouseMovementsRepository : IRepository<WarehouseMovements>
    {
        IQueryable<WarehouseMovements> GetAll();  
        SaveResult<WarehouseMovements> CreateWarehouseMovements(WarehouseMovements entity);  
    }
}