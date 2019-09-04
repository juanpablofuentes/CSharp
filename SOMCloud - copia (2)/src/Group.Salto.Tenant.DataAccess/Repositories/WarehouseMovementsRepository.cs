using System.Linq;
using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class WarehouseMovementsRepository : BaseRepository<WarehouseMovements>, IWarehouseMovementsRepository
    {
        public WarehouseMovementsRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<WarehouseMovements> GetAll()
        {
            return All();
        }

        public SaveResult<WarehouseMovements> CreateWarehouseMovements(WarehouseMovements entity)
        {
            Create(entity);
            var result = SaveChange(entity);
            return result; 
        }
    }
}